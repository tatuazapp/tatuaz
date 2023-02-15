using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;

namespace Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

public interface IGenericRepository<TEntity, THistEntity, in TId> : IDisposable
    where TEntity : Entity<THistEntity, TId>, new()
    where THistEntity : HistEntity<TId>, new()
    where TId : notnull
{
    void ExplicitlyUseDbContext(DbContext dbContext);

    Task<TEntity?> GetByIdAsync(
        TId id,
        bool track = false,
        CancellationToken cancellationToken = default
    );

    Task<TDto?> GetByIdAsync<TDto>(TId id, CancellationToken cancellationToken = default)
        where TDto : class;

    Task<TEntity?> GetByIdAsync(
        TId id,
        Instant asOf,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdAsync(TId id, Instant asOf, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetBySpecificationAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<TDto>> GetBySpecificationAsync<TDto>(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default
    )
        where TDto : class;

    Task<IEnumerable<TEntity>> GetBySpecificationAsync(
        ISpecification<TEntity> specification,
        Instant asOf,
        CancellationToken cancellationToken = default
    );

    Task<PagedData<TEntity>> GetBySpecificationWithPagingAsync(
        ISpecification<TEntity> specification,
        PagedParams pagedParams,
        CancellationToken cancellationToken = default
    );

    Task<PagedData<TDto>> GetBySpecificationWithPagingAsync<TDto>(
        ISpecification<TEntity> specification,
        PagedParams pagedParams,
        CancellationToken cancellationToken = default
    )
        where TDto : class;

    Task<PagedData<TEntity>> GetBySpecificationWithPagingAsync(
        ISpecification<TEntity> specification,
        PagedParams pagedParams,
        Instant asOf,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Instant asOf,
        CancellationToken cancellationToken = default
    );

    Task<long> CountByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    );

    Task<long> CountByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Instant asOf,
        CancellationToken cancellationToken = default
    );

    void Create(TEntity entity);
    void Delete(TEntity entity);
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
}
