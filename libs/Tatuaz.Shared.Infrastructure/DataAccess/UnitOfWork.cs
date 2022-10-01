using Microsoft.EntityFrameworkCore;

using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Infrastructure.Abstractions;

namespace Tatuaz.Shared.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly IServiceProvider _services;
    private readonly IUserAccessor _userAccessor;

    public UnitOfWork(DbContext context, IServiceProvider services, IUserAccessor userAccessor)
    {
        _context = context;
        _services = services;
        _userAccessor = userAccessor;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateUserContext();
        SaveHistChanges();
        var changes = await _context.SaveChangesAsync(cancellationToken);
        await CommitHistChanges();
        return changes;
    }

    public async Task RunInTransactionAsync(Func<CancellationToken, Task> action, Action<Exception>? onFailure = null,
        bool rollbackOnFailure = true, CancellationToken cancellationToken = default)
    {
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            UpdateUserContext();
            SaveHistChanges();
            await action(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            await CommitHistChanges();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            await RollbackHistChanges();
            onFailure?.Invoke(e);
        }
    }

    private void SaveHistChanges()
    {
        // TODO: change when historical microservice is up
    }

    private Task CommitHistChanges()
    {
        // TODO: change when historical microservice is up
        return Task.CompletedTask;
    }

    private Task RollbackHistChanges()
    {
        // TODO: change when historical microservice is up
        return Task.CompletedTask;
    }

    private void UpdateUserContext()
    {
        var auditableEntities = _context.ChangeTracker.Entries<IAuditableEntity>().ToList();
        foreach (var entity in auditableEntities.Where(x => x.State == EntityState.Added))
            entity.Entity.UpdateCreationData(_userAccessor.CurrentUserId);
        foreach (var entity in auditableEntities.Where(x => x.State is EntityState.Modified))
            entity.Entity.UpdateModificationData(_userAccessor.CurrentUserId);
    }
}
