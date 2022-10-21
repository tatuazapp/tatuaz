using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Common;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;

namespace Tatuaz.Shared.Infrastructure.DataAccess;

public class GenericRepository<TEntity, THistEntity, TId>
    : IGenericRepository<TEntity, THistEntity, TId>
    where TEntity : Entity<THistEntity, TId>, new()
    where THistEntity : HistEntity<TId>, new()
    where TId : notnull
{
    private readonly DbContext _dbContext;

    public GenericRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(
        TId id,
        bool track = false,
        CancellationToken cancellationToken = default
    )
    {
        var baseQuery = _dbContext.Set<TEntity>().AsQueryable();
        if (!track)
            baseQuery = baseQuery.AsNoTracking();

        return await baseQuery
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<TEntity?> GetByIdAsync(
        TId id,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        // TODO: change when historical microservice is up
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var baseQuery = _dbContext.Set<TEntity>().AsQueryable();
        return await baseQuery
            .AnyAsync(x => x.Id.Equals(id), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<bool> ExistsByIdAsync(
        TId id,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        // TODO: change when historical microservice is up
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TEntity>> GetBySpecificationAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default
    )
    {
        return await specification
            .Apply(_dbContext.Set<TEntity>())
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<TEntity>> GetBySpecificationAsync(
        ISpecification<TEntity> specification,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        // TODO: change when historical microservice is up
        throw new NotImplementedException();
    }

    public async Task<PagedData<TEntity>> GetBySpecificationWithPagingAsync(
        ISpecification<TEntity> specification,
        PagedParams pagedParams,
        CancellationToken cancellationToken = default
    )
    {
        var baseQuery = specification.Apply(_dbContext.Set<TEntity>());
        var toSkip = (pagedParams.PageNumber - 1) * pagedParams.PageSize;

        var data = await baseQuery
            .Skip(toSkip)
            .Take(pagedParams.PageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var count = await baseQuery.CountAsync(cancellationToken).ConfigureAwait(false);

        var totalPages = (int)Math.Ceiling(count / (float)pagedParams.PageSize);

        return new PagedData<TEntity>(
            data,
            pagedParams.PageSize,
            pagedParams.PageSize,
            totalPages,
            count
        );
    }

    public async Task<PagedData<TEntity>> GetBySpecificationWithPagingAsync(
        ISpecification<TEntity> specification,
        PagedParams pagedParams,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        // TODO: change when historical microservice is up
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        return await _dbContext
            .Set<TEntity>()
            .AnyAsync(predicate, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<bool> ExistsByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        // TODO: change when historical microservice is up
        throw new NotImplementedException();
    }

    public async Task<long> CountByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        return await _dbContext
            .Set<TEntity>()
            .CountAsync(predicate, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<long> CountByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        // TODO: change when historical microservice is up
        throw new NotImplementedException();
    }

    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TEntity>().Add(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        var toDelete = await _dbContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken)
            .ConfigureAwait(false);
        if (toDelete == null)
            return;

        _dbContext.Set<TEntity>().Remove(toDelete);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }
}
