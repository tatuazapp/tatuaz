using System.Linq.Expressions;

using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Domain.Models.Hist.Common;

namespace Tatuaz.Shared.Infrastructure.Abstractions;

public interface IGenericRepository<TEntity, THistEntity, in TId>
    where TEntity : Entity<THistEntity, TId>, new()
    where THistEntity : HistEntity<TId>, new()
    where TId : notnull
{
    Task<TEntity?> GetByIdAsync(TId id, bool track = false,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(TId id, DateTime asOf,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdAsync(TId id, bool track = false,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdAsync(TId id, DateTime asOf,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetBySpecificationAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetByPredicateAsync(ISpecification<TEntity> specification, DateTime asOf,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByPredicateAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByPredicateAsync(ISpecification<TEntity> specification, DateTime asOf,
        CancellationToken cancellationToken = default);

    Task<long> CountByPredicateAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<long> CountByPredicateAsync(ISpecification<TEntity> specification, DateTime asOf,
        CancellationToken cancellationToken = default);

    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
