using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NodaTime;

namespace Tatuaz.History.Queue.Contracts;

public record HistGetByPredicateOrder<TEntity, TId>(
    Expression<Func<TEntity, bool>> Predicate,
    Func<IEnumerable<TEntity>, IOrderedEnumerable<TEntity>> OrderBy,
    Instant AsOf
) where TEntity : class;

public record HistGetByPredicateResult<TEntity, TId>(IEnumerable<TEntity> Entity)
    where TEntity : class;