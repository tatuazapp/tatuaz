using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using Tatuaz.History.Queue;
using Tatuaz.History.Queue.Util;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Shared.Infrastructure.DataAccess;

public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    private readonly IClock _clock;
    private readonly TDbContext _context;
    private readonly List<HistEntity> _histEntitiesToDump;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IUserAccessor _userAccessor;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(
        TDbContext context,
        IUserAccessor userAccessor,
        IClock clock,
        ISendEndpointProvider sendEndpointProvider
    )
    {
        _context = context;
        _userAccessor = userAccessor;
        _clock = clock;
        _sendEndpointProvider = sendEndpointProvider;
        _currentTransaction = null;
        _histEntitiesToDump = new List<HistEntity>();
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

    public async Task RunInTransactionAsync(
        Func<CancellationToken, Task> action,
        Action<Exception>? onFailure = null,
        CancellationToken cancellationToken = default
    )
    {
        _currentTransaction = await _context.Database
            .BeginTransactionAsync(cancellationToken)
            .ConfigureAwait(false);
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
        var dumpHistoryOrders = _histEntitiesToDump
            .Select(HistorySerializer.SerializeDumpHistoryOrder)
            .ToImmutableArray();

        if (dumpHistoryOrders.Any())
        {
            var endpoint = await _sendEndpointProvider
                .GetSendEndpoint(HistoryQueueConstants.DumpQueueUri)
                .ConfigureAwait(false);

            await Task.WhenAll(dumpHistoryOrders.Select(x => endpoint.Send(x, cancellationToken)))
                .ConfigureAwait(false);
        }

        _histEntitiesToDump.Clear();
    }

    private IEnumerable<HistEntity> GetDumpHistoryOrders()
    {
        return _context.ChangeTracker
            .Entries<IHistDumpableEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(x => x.Entity.ToHistEntity(_clock, HistStateFromEntityState(x.State)))
            .ToImmutableArray();
    }

    private static HistState HistStateFromEntityState(EntityState entityState)
    {
        return entityState switch
        {
            EntityState.Added => HistState.Added,
            EntityState.Modified => HistState.Modified,
            EntityState.Deleted => HistState.Deleted,
            _ => throw new ArgumentOutOfRangeException(nameof(entityState), entityState, "Invalid entity state")
        };
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
