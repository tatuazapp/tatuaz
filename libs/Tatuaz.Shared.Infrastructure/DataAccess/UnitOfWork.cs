using System.Text.Json;

using MassTransit;
using MassTransit.Internals;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using NodaTime;
using NodaTime.Serialization.JsonNet;

using Tatuaz.History.Queue.Consumers;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Common;
using Tatuaz.Shared.Infrastructure.Abstractions;

using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Tatuaz.Shared.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IClock _clock;
    private readonly ISendEndpointProvider _endpointProvider;
    private readonly IRequestClient<DumpHistoryOrder> _mtClient;
    private readonly DbContext _context;
    private readonly IUserAccessor _userAccessor;

    public UnitOfWork(DbContext context, IUserAccessor userAccessor, IClock clock,
        ISendEndpointProvider endpointProvider)
    {
        _context = context;
        _userAccessor = userAccessor;
        _clock = clock;
        _endpointProvider = endpointProvider;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateUserContext();
        var changes = await _context.SaveChangesAsync(cancellationToken);
        await DumpHistoryChanges(cancellationToken);
        return changes;
    }

    public async Task RunInTransactionAsync(Func<CancellationToken, Task> action, Action<Exception>? onFailure = null,
        bool rollbackOnFailure = true, CancellationToken cancellationToken = default)
    {
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            UpdateUserContext();
            await action(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            await DumpHistoryChanges(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            onFailure?.Invoke(e);
        }
    }

    private async Task DumpHistoryChanges(CancellationToken cancellationToken = default)
    {
        var jsonSerializer = new JsonSerializer();
        jsonSerializer.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

        var entriesToDump = _context
            .ChangeTracker
            .Entries<IHistDumpableEntity>()
            .Select(x => x.Entity.ToHistEntity(_clock))
            .Select(x => new DumpHistoryOrder(x.GetType().ToString(),
                JsonConvert.SerializeObject(x, jsonSerializer.Formatting)))
            .ToList();

        if (entriesToDump.Any())
        {
            var endpoint = await _endpointProvider.GetSendEndpoint(DumpHistoryConsumer.Uri);

            await endpoint.SendBatch(entriesToDump, cancellationToken);
        }
    }

    private void UpdateUserContext()
    {
        var auditableEntries = _context.ChangeTracker.Entries<IAuditableEntity>().ToList();
        foreach (var entry in auditableEntries.Where(x => x.State == EntityState.Added))
            entry.Entity.UpdateCreationData(_userAccessor.CurrentUserId, _clock);
        foreach (var entry in auditableEntries.Where(x => x.State is EntityState.Modified))
            entry.Entity.UpdateModificationData(_userAccessor.CurrentUserId, _clock);
    }
}
