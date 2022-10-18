using NodaTime;

namespace Tatuaz.Shared.Domain.Entities.Hist.Common;

public class HistEntity<TId> : HistEntity
    where TId : notnull
{
    public TId Id { get; set; } = default!;
}

public class HistEntity
{
    public Guid HistId { get; set; }
    public HistState HistState { get; set; }
    public Instant HistDumpedAt { get; set; }
}

public enum HistState
{
    Added,
    Modified,
    Deleted
}
