using System.Collections.Immutable;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Tatuaz.History.Queue.Consumers;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Common;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Shared.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly IClock _clock;
    private readonly DbContext _context;
    private readonly List<IHistDumpableEntity> _histEntitiesToDump;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IUserAccessor _userAccessor;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(DbContext context, IUserAccessor userAccessor, IClock clock,
        ISendEndpointProvider sendEndpointProvider)
    {
        _context = context;
        _userAccessor = userAccessor;
        _clock = clock;
        _sendEndpointProvider = sendEndpointProvider;
        _currentTransaction = null;
        _histEntitiesToDump = new List<IHistDumpableEntity>();
    }

    public void Dispose()
    {
        _context.Dispose();
        _currentTransaction = null;
        GC.SuppressFinalize(this);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateUserContext();
        _histEntitiesToDump.AddRange(GetDumpHistoryOrders());
        var changes = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (_currentTransaction == null)
        {
            await DumpHistoryChanges(cancellationToken).ConfigureAwait(false);
        }

        return changes;
    }

    public async Task RunInTransactionAsync(Func<CancellationToken, Task> action, Action<Exception>? onFailure = null,
        bool rollbackOnFailure = true, CancellationToken cancellationToken = default)
    {
        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            UpdateUserContext();
            await action(cancellationToken).ConfigureAwait(false);
            _histEntitiesToDump.AddRange(GetDumpHistoryOrders());
            await _currentTransaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            await DumpHistoryChanges(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            await _currentTransaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            onFailure?.Invoke(e);
        }
        finally
        {
            _currentTransaction.Dispose();
        }
    }

    private async Task DumpHistoryChanges(CancellationToken cancellationToken = default)
    {
        var jsonSerializer = new JsonSerializer();
        jsonSerializer.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

        var dumpHistoryOrders = _histEntitiesToDump.Select(x => x.ToHistEntity(_clock))
            .Select(x => new DumpHistoryOrder(x.GetType().ToString(),
                JsonConvert.SerializeObject(x, jsonSerializer.Formatting)))
            .ToImmutableArray();

        if (dumpHistoryOrders.Any())
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(DumpHistoryConsumer.Uri).ConfigureAwait(false);

            await Task.WhenAll(dumpHistoryOrders.Select(x => endpoint.Send(x, cancellationToken)))
                .ConfigureAwait(false);
        }

        _histEntitiesToDump.Clear();
    }

    private IEnumerable<IHistDumpableEntity> GetDumpHistoryOrders()
    {
        return _context
            .ChangeTracker
            .Entries<IHistDumpableEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(x => x.Entity)
            .ToImmutableArray();
    }

    private void UpdateUserContext()
    {
        var auditableEntries = _context.ChangeTracker.Entries<IAuditableEntity>().ToList();
        foreach (var entry in auditableEntries.Where(x => x.State == EntityState.Added))
        {
            entry.Entity.UpdateCreationData(_userAccessor.CurrentUserId, _clock);
        }

        foreach (var entry in auditableEntries.Where(x => x.State is EntityState.Modified))
        {
            entry.Entity.UpdateModificationData(_userAccessor.CurrentUserId, _clock);
        }
    }
}
