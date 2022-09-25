using System.Security.Principal;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Domain.Models.Hist.Common;
using Tatuaz.Shared.Infrastructure.Abstractions;

namespace Tatuaz.Shared.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly Dictionary<Type, object> _repositories;
    private readonly IServiceProvider _services;
    private readonly IUserAccessor _userAccessor;

    public UnitOfWork(DbContext context, IServiceProvider services, IUserAccessor userAccessor)
    {
        _context = context;
        _services = services;
        _userAccessor = userAccessor;
        _repositories = new Dictionary<Type, object>();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public IGenericRepository<TEntity, THistEntity, TId> GetRepository<TEntity, THistEntity, TId>()
        where TEntity : Entity<THistEntity, TId>, new()
        where TId : HistEntity<THistEntity>, new()
        where THistEntity : HistEntity<TId>, new()
    {
        if (_repositories.TryGetValue(typeof(TEntity), out var inDictRepository))
            return (IGenericRepository<TEntity, THistEntity, TId>)inDictRepository;

        var inServiceRepository = _services.GetService<IGenericRepository<TEntity, THistEntity, TId>>();
        if (inServiceRepository != null)
        {
            _repositories.Add(typeof(TEntity), inServiceRepository);
            return inServiceRepository;
        }

        var createdRepository = new GenericRepository<TEntity, THistEntity, TId>(_context);
        _repositories.Add(typeof(TEntity), createdRepository);
        return createdRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateUserContext();
        SaveHistChanges();
        var changes = await _context.SaveChangesAsync(cancellationToken);
        CommitHistChanges();
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
        Thread.CurrentPrincipal =
            new GenericPrincipal(new GenericIdentity(_userAccessor.CurrentUserId.ToString()), null);
        var auditableEntities = _context.ChangeTracker.Entries<IAuditableEntity>().ToList();
        foreach (var entity in auditableEntities.Where(x => x.State == EntityState.Added))
            entity.Entity.UpdateCreationData(_userAccessor.CurrentUserId);
        foreach (var entity in auditableEntities.Where(x => x.State is EntityState.Modified or EntityState.Deleted))
            entity.Entity.UpdateModificationData(_userAccessor.CurrentUserId);
    }
}
