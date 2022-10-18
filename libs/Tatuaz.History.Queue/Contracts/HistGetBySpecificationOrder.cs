using NodaTime;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;

namespace Tatuaz.History.Queue.Contracts;

public record HistGetBySpecificationOrder<TEntity, TId>(ISpecification<TEntity> Specification, Instant AsOf)
    where TEntity : class;

public record HistGetBySpecificationResult<TEntity, TId>(IEnumerable<TEntity> Entity)
    where TEntity : class;
