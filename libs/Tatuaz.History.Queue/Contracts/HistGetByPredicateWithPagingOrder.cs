using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NodaTime;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;

namespace Tatuaz.History.Queue.Contracts;

public record HistGetByPredicateWithPagingOrder<TEntity, TId>(
    Expression<Func<TEntity, bool>> Predicate,
    Func<IEnumerable<TEntity>, IOrderedEnumerable<TEntity>> OrderBy,
    PagedParams PagedParams,
    Instant AsOf
) where TEntity : class;

public record HistGetByPredicateWithPagingResult<TEntity, TId>(PagedData<TEntity> PagedData)
    where TEntity : class;
