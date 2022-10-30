using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;

namespace Tatuaz.History.DataAccess.Services;

public interface IHistorySearcherService<TEntity, in TId>
    where TEntity : HistEntity<TId>
    where TId : notnull
{
    Task<TEntity?> GetByIdAsync(
        TId id,
        Instant asOf,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByIdAsync(TId id, Instant asOf, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IEnumerable<TEntity>, IOrderedEnumerable<TEntity>> orderBy,
        Instant asOf,
        CancellationToken cancellationToken = default
    );

    Task<PagedData<TEntity>> GetByPredicateWithPagingAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IEnumerable<TEntity>, IOrderedEnumerable<TEntity>> orderBy,
        PagedParams pagedParams,
        Instant asOf,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Instant asOf,
        CancellationToken cancellationToken = default
    );

    Task<long> CountByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Instant asOf,
        CancellationToken cancellationToken = default
    );
}
