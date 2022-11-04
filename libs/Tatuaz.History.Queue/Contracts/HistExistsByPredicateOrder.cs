using System;
using System.Linq.Expressions;
using NodaTime;

namespace Tatuaz.History.Queue.Contracts;

public record HistExistsByPredicateOrder<TEntity, TId>(
    Expression<Func<TEntity, bool>> Predicate,
    Instant AsOf
);

public record HistExistsByPredicateResult(bool Exists);