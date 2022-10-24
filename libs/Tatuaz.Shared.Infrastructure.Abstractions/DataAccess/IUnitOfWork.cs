using Microsoft.EntityFrameworkCore;

namespace Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

public interface IUnitOfWork<TDbContext> : IDisposable
    where TDbContext : DbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task RunInTransactionAsync(
        Func<CancellationToken, Task> action,
        Action<Exception>? onFailure = null,
        CancellationToken cancellationToken = default
    );
}
