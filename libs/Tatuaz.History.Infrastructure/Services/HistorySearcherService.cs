using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Common;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;

namespace Tatuaz.History.DataAccess.Services;

public class HistorySearcherService<TEntity, TId> : IHistorySearcherService<TEntity, TId>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    private readonly HistDbContext _histDbContext;

    public HistorySearcherService(HistDbContext histDbContext)
    {
        _histDbContext = histDbContext;
    }

    public async Task<TEntity?> GetByIdAsync(TId id, Instant asOf, CancellationToken cancellationToken = default)
    {
        var entity = await _histDbContext.Set<TEntity>()
            .AsNoTracking()
            .Where(x => x.Id.Equals(id) && x.HistDumpedAt < asOf)
            .OrderByDescending(x => x.HistDumpedAt)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return entity?.HistState == HistState.Deleted ? null : entity;
    }

    public async Task<bool> ExistsByIdAsync(TId id, Instant asOf, CancellationToken cancellationToken = default)
    {
        var entity = await _histDbContext.Set<TEntity>()
            .AsNoTracking()
            .Where(x => x.Id.Equals(id) && x.HistDumpedAt < asOf)
            .OrderByDescending(x => x.HistDumpedAt)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return entity?.HistState != HistState.Deleted && entity != null;
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

    public async Task<bool> ExistsByPredicateAsync(Expression<Func<TEntity, bool>> predicate, Instant asOf,
        CancellationToken cancellationToken = default)
    {
        return (await _histDbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(x => x.HistDumpedAt < asOf)
                .Where(predicate)
                .OrderByDescending(x => x.HistDumpedAt)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false))
            .Aggregate(new List<TEntity>(),
                (collection, entity)
                    => collection.Any(x => x.Id.Equals(entity.Id))
                        ? collection
                        : collection.Append(entity).ToList())
            .Any(x => x.HistState != HistState.Deleted);
    }

    public async Task<long> CountByPredicateAsync(Expression<Func<TEntity, bool>> predicate, Instant asOf,
        CancellationToken cancellationToken = default)
    {
        return (await _histDbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(x => x.HistDumpedAt < asOf)
                .Where(predicate)
                .OrderByDescending(x => x.HistDumpedAt)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false))
            .Aggregate(new List<TEntity>(),
                (collection, entity)
                    => collection.Any(x => x.Id.Equals(entity.Id))
                        ? collection
                        : collection.Append(entity).ToList())
            .Count(x => x.HistState != HistState.Deleted);
    }
}