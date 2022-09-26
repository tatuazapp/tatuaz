namespace Tatuaz.Shared.Infrastructure.Abstractions;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task RunInTransactionAsync(Func<CancellationToken, Task> action, Action<Exception>? onFailure = null,
        bool rollbackOnFailure = true, CancellationToken cancellationToken = default);
}
