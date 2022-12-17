using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;
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
    private DbContext _dbContext;
    private readonly IMapper _mapper;

    public GenericRepository(DbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    ///     Use only if you want to use other dbContext than configured in DI container.
    /// </summary>
    /// <example>
    ///     <code>
    /// using(var scope = _scopeFactory.CreateScope())
    /// {
    ///     var dbContext = scope.ServiceProvider.GetRequiredService<Example_db_context>
    ///             ();
    ///             // use dbContext
    ///             }
    ///             // here this repository won't work if you want
    ///             // to use it again you have to call ExplicitlyUseDbContext method
    ///             // with dbContext from DI container
    /// </code>
    /// </example>
    /// <param name="dbContext"></param>
    public void ExplicitlyUseDbContext(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(
        TId id,
        bool track = false,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> baseQuery = _dbContext.Set<TEntity>();
        if (!track)
        {
            baseQuery = baseQuery.AsNoTracking();
        }

        return await baseQuery
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<TDto?> GetByIdAsync<TDto>(
        TId id,
        CancellationToken cancellationToken = default
    ) where TDto : class
    {
        var result = await _dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken)
            .ConfigureAwait(false);

        return result is null ? null : _mapper.Map<TDto>(result);
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
        var baseQuery = _dbContext.Set<TEntity>();
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

    public async Task<IEnumerable<TDto>> GetBySpecificationAsync<TDto>(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default
    ) where TDto : class
    {
        return await specification
            .Apply(_dbContext.Set<TEntity>())
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
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

    public async Task<PagedData<TDto>> GetBySpecificationWithPagingAsync<TDto>(
        ISpecification<TEntity> specification,
        PagedParams pagedParams,
        CancellationToken cancellationToken = default
    ) where TDto : class
    {
        var baseQuery = specification.Apply(_dbContext.Set<TEntity>());
        var toSkip = (pagedParams.PageNumber - 1) * pagedParams.PageSize;

        var data = await baseQuery
            .Skip(toSkip)
            .Take(pagedParams.PageSize)
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var count = await baseQuery.CountAsync(cancellationToken).ConfigureAwait(false);

        var totalPages = (int)Math.Ceiling(count / (float)pagedParams.PageSize);

        return new PagedData<TDto>(
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

    public void Create(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        var toDelete = await _dbContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken)
            .ConfigureAwait(false);
        if (toDelete == null)
        {
            return;
        }

        _dbContext.Set<TEntity>().Remove(toDelete);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
