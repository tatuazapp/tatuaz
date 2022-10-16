using System.Linq.Expressions;

using NodaTime;

using Tatuaz.Shared.Domain.Entities.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Common;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;

namespace Tatuaz.Shared.Infrastructure.Abstractions;

public interface IGenericRepository<TEntity, THistEntity, in TId>
    where TEntity : Entity<THistEntity, TId>, new()
    where THistEntity : HistEntity<TId>, new()
    where TId : notnull
{
    Task<TEntity?> GetByIdAsync(TId id, bool track = false,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(TId id, Instant asOf,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdAsync(TId id,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdAsync(TId id, Instant asOf,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetBySpecificationAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetBySpecificationAsync(ISpecification<TEntity> specification, Instant asOf,
        CancellationToken cancellationToken = default);

    Task<PagedData<TEntity>> GetBySpecificationWithPagingAsync(ISpecification<TEntity> specification,
        PagedParams pagedParams,
        CancellationToken cancellationToken = default);

    Task<PagedData<TEntity>> GetBySpecificationWithPagingAsync(ISpecification<TEntity> specification,
        PagedParams pagedParams, Instant asOf,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByPredicateAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByPredicateAsync(Expression<Func<TEntity, bool>> predicate, Instant asOf,
        CancellationToken cancellationToken = default);

    Task<long> CountByPredicateAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<long> CountByPredicateAsync(Expression<Func<TEntity, bool>> predicate, Instant asOf,
        CancellationToken cancellationToken = default);

    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
}
