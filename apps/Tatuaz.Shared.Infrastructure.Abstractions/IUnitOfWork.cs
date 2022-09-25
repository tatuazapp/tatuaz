using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Domain.Models.Hist.Common;

namespace Tatuaz.Shared.Infrastructure.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity, THistEntity, TId> GetRepository<TEntity, THistEntity, TId>()
        where TEntity : Entity<THistEntity, TId>, new()
        where TId : HistEntity<THistEntity>, new()
        where THistEntity : HistEntity<TId>, new();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task RunInTransactionAsync(Func<CancellationToken, Task> action, Action<Exception>? onFailure = null,
        bool rollbackOnFailure = true, CancellationToken cancellationToken = default);
}
