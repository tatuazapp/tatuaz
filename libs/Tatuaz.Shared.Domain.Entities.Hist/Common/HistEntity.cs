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

    public Instant HistFrom { get; set; }

    public Instant? HistTo { get; set; }
}
