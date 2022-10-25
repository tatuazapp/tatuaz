using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;

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

    public async Task<TEntity?> GetByIdAsync(
        TId id,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        var entity = await _histDbContext
            .Set<TEntity>()
            .AsNoTracking()
            .Where(x => x.Id.Equals(id) && x.HistDumpedAt < asOf)
            .OrderByDescending(x => x.HistDumpedAt)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return entity?.HistState == HistState.Deleted ? null : entity;
    }

    public async Task<bool> ExistsByIdAsync(
        TId id,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        var entity = await _histDbContext
            .Set<TEntity>()
            .AsNoTracking()
            .Where(x => x.Id.Equals(id) && x.HistDumpedAt < asOf)
            .OrderByDescending(x => x.HistDumpedAt)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        return entity?.HistState != HistState.Deleted && entity != null;
    }

    public async Task<IEnumerable<TEntity>> GetByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IEnumerable<TEntity>, IOrderedEnumerable<TEntity>> orderBy,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        return orderBy(
                await _histDbContext
                    .Set<TEntity>()
                    .AsNoTracking()
                    .Where(x => x.HistDumpedAt < asOf)
                    .Where(predicate)
                    .OrderByDescending(x => x.HistDumpedAt)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
            )
            .Aggregate(
                new List<TEntity>(),
                (collection, entity) =>
                    collection.Any(x => x.Id.Equals(entity.Id))
                        ? collection
                        : collection.Append(entity).ToList()
            )
            .ToList();
    }

    public async Task<PagedData<TEntity>> GetByPredicateWithPagingAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IEnumerable<TEntity>, IOrderedEnumerable<TEntity>> orderBy,
        PagedParams pagedParams,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        var allData = orderBy(
                (
                    await _histDbContext
                        .Set<TEntity>()
                        .AsNoTracking()
                        .Where(x => x.HistDumpedAt < asOf)
                        .Where(predicate)
                        .OrderByDescending(x => x.HistDumpedAt)
                        .ToListAsync(cancellationToken)
                        .ConfigureAwait(false)
                ).Aggregate(
                    new List<TEntity>(),
                    (collection, entity) =>
                        collection.Any(x => x.Id.Equals(entity.Id))
                            ? collection
                            : collection.Append(entity).ToList()
                )
            )
            .ToList();

        var toSkip = (pagedParams.PageNumber - 1) * pagedParams.PageSize;

        var data = allData.Skip(toSkip).Take(pagedParams.PageSize).ToList();

        var count = allData.Count;

        var totalPages = (int)Math.Ceiling(count / (float)pagedParams.PageSize);

        return new PagedData<TEntity>(
            data,
            pagedParams.PageSize,
            pagedParams.PageSize,
            totalPages,
            count
        );
    }

    public async Task<bool> ExistsByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        return (
                await _histDbContext
                    .Set<TEntity>()
                    .AsNoTracking()
                    .Where(x => x.HistDumpedAt < asOf)
                    .Where(predicate)
                    .OrderByDescending(x => x.HistDumpedAt)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
            )
            .Aggregate(
                new List<TEntity>(),
                (collection, entity) =>
                    collection.Any(x => x.Id.Equals(entity.Id))
                        ? collection
                        : collection.Append(entity).ToList()
            )
            .Any(x => x.HistState != HistState.Deleted);
    }

    public async Task<long> CountByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Instant asOf,
        CancellationToken cancellationToken = default
    )
    {
        return (
                await _histDbContext
                    .Set<TEntity>()
                    .AsNoTracking()
                    .Where(x => x.HistDumpedAt < asOf)
                    .Where(predicate)
                    .OrderByDescending(x => x.HistDumpedAt)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
            )
            .Aggregate(
                new List<TEntity>(),
                (collection, entity) =>
                    collection.Any(x => x.Id.Equals(entity.Id))
                        ? collection
                        : collection.Append(entity).ToList()
            )
            .Count(x => x.HistState != HistState.Deleted);
    }
}
