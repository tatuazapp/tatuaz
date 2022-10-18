using NodaTime;

namespace Tatuaz.History.Queue.Contracts;

public record HistGetByIdOrder<TEntity, TId>(TId Id, Instant AsOf);

public record HistGetByIdResult<TEntity, TId>(TEntity? Entity);
