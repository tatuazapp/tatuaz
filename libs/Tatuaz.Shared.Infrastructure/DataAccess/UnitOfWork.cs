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
using Tatuaz.Shared.Pipeline.Exceptions;
using Tatuaz.Shared.Pipeline.UserContext;
using static System.GC;

namespace Tatuaz.Shared.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly IClock _clock;
    private readonly List<HistEntity> _histEntitiesToDump;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IUserContext _userContext;
    private IDbContextTransaction? _currentTransaction;
    private DbContext _dbContext;

    public UnitOfWork(
        DbContext dbContext,
        IUserContext userContext,
        IClock clock,
        ISendEndpointProvider sendEndpointProvider
    )
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _clock = clock;
        _sendEndpointProvider = sendEndpointProvider;
        _currentTransaction = null;
        _histEntitiesToDump = new List<HistEntity>();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        _currentTransaction = null;
        SuppressFinalize(this);
    }

    /// <summary>
    ///     Use only if you want to use other dbContext than configured in DI container.
    /// </summary>
    /// <example>
    ///     <code>
    /// using(var scope = _scopeFactory.CreateScope())
    /// {
    ///     var dbContext = scope.ServiceProvider.GetRequiredService<Example_db_context>
    ///             ();
    ///             // use dbContext
    ///             }
    ///             // here this uow won't work if you want
    ///             // to use it again you have to call ExplicitlyUseDbContext method
    ///             // with dbContext from DI container
    /// </code>
    /// </example>
    /// <param name="dbContext"></param>
    public void ExplicitlyUseDbContext(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateUserContext();
        _histEntitiesToDump.AddRange(GetDumpHistoryOrders());
        var changes = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
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
        _currentTransaction = await _dbContext.Database
            .BeginTransactionAsync(cancellationToken)
            .ConfigureAwait(false);
        try
        {
            UpdateUserContext();
            await action(cancellationToken).ConfigureAwait(false);
            _histEntitiesToDump.AddRange(GetDumpHistoryOrders());
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
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
                .GetSendEndpoint(HistoryQueueConstants.DumpHistoryQueueUri)
                .ConfigureAwait(false);

            await Task.WhenAll(dumpHistoryOrders.Select(x => endpoint.Send(x, cancellationToken)))
                .ConfigureAwait(false);
        }

        _histEntitiesToDump.Clear();
    }

    private IEnumerable<HistEntity> GetDumpHistoryOrders()
    {
        return _dbContext.ChangeTracker
            .Entries<IHistDumpableEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(x => x.Entity.ToHistEntity(_clock, HistStateFromEntityState(x.State)))
            .ToArray();
    }

    private static HistState HistStateFromEntityState(EntityState entityState)
    {
        return entityState switch
        {
            EntityState.Added => HistState.Added,
            EntityState.Modified => HistState.Modified,
            EntityState.Deleted => HistState.Deleted,
            _
                => throw new ArgumentOutOfRangeException(
                    nameof(entityState),
                    entityState,
                    "Invalid entity state"
                )
        };
    }

    private void UpdateUserContext()
    {
        var auditableEntries = _dbContext.ChangeTracker.Entries<IAuditableEntity>().ToList();
        var userId = _userContext.CurrentUserEmail ?? throw new UserContextMissingException();

        foreach (var entry in auditableEntries.Where(x => x.State == EntityState.Added))
        {
            entry.Entity.UpdateCreationData(userId, _clock);
        }

        foreach (var entry in auditableEntries.Where(x => x.State is EntityState.Modified))
        {
            entry.Entity.UpdateModificationData(userId, _clock);
        }
    }
}
