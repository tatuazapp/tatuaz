using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Attributes;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

[BaseHistEntity, TestIgnoreHistEntity]
public class HistEntity<TId> : HistEntity, IHistEntity where TId : notnull
{
    public TId Id { get; set; } = default!;
}

[BaseHistEntity, TestIgnoreHistEntity]
public class HistEntity : IHistEntity
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
