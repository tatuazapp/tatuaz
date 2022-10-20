using System.Linq.Expressions;
using NodaTime;

namespace Tatuaz.History.Queue.Contracts;

public record HistCountByPredicateOrder<TEntity, TId>(
    Expression<Func<TEntity, bool>> Predicate,
    Instant AsOf
);

public record HistCountByPredicateResult(int Count);
