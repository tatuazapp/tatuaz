using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Attributes;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

[BaseHistEntity]
public class HistEntity<TId> : HistEntity where TId : notnull
{
    public TId Id { get; set; } = default!;
}

[BaseHistEntity]
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
