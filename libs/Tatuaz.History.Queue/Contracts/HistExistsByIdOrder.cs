using NodaTime;

namespace Tatuaz.History.Queue.Contracts;

public record HistExistsByIdOrder<TEntity, TId>(TId Id, Instant AsOf);

public record HistExistsByIdResult<TEntity, TId>(bool Exists);