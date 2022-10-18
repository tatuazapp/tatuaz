using System.Linq.Expressions;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Common;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;

namespace Tatuaz.History.DataAccess.Services;

public interface IHistorySearcherService<TEntity, in TId>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    Task<TEntity?> GetByIdAsync(TId id, Instant asOf,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdAsync(TId id, Instant asOf,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetBySpecificationAsync(ISpecification<TEntity> specification, Instant asOf,
        CancellationToken cancellationToken = default);

    Task<PagedData<TEntity>> GetBySpecificationWithPagingAsync(ISpecification<TEntity> specification,
        PagedParams pagedParams, Instant asOf,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByPredicateAsync(Expression<Func<TEntity, bool>> predicate, Instant asOf,
        CancellationToken cancellationToken = default);

    Task<long> CountByPredicateAsync(Expression<Func<TEntity, bool>> predicate, Instant asOf,
        CancellationToken cancellationToken = default);
}