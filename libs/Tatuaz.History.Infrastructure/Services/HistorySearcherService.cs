using System.Linq.Expressions;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Common;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;

namespace Tatuaz.History.DataAccess.Services;

public class HistorySearcherService<TEntity, TId> : IHistorySearcherService<TEntity, TId>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    public Task<TEntity?> GetByIdAsync(TId id, Instant asOf, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByIdAsync(TId id, Instant asOf, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> GetBySpecificationAsync(ISpecification<TEntity> specification, Instant asOf,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PagedData<TEntity>> GetBySpecificationWithPagingAsync(ISpecification<TEntity> specification,
        PagedParams pagedParams, Instant asOf,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByPredicateAsync(Expression<Func<TEntity, bool>> predicate, Instant asOf,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<long> CountByPredicateAsync(Expression<Func<TEntity, bool>> predicate, Instant asOf,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
