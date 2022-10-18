using NodaTime;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;

namespace Tatuaz.History.Queue.Contracts;

public record HistGetBySpecificationWithPagingOrder<TEntity, TId>(ISpecification<TEntity> Specification,
    PagedParams PagedParams, Instant AsOf)
    where TEntity : class;

public record HistGetBySpecificationWithPagingResult<TEntity, TId>(PagedData<TEntity> PagedData)
    where TEntity : class;