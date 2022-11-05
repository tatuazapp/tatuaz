using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

public interface IUnitOfWork : IDisposable
{
    void ExplicitlyUseDbContext(DbContext dbContext);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task RunInTransactionAsync(
        Func<CancellationToken, Task> action,
        Action<Exception>? onFailure = null,
        CancellationToken cancellationToken = default
    );
}
