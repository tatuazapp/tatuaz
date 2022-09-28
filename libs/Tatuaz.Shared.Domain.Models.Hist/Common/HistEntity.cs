using System.ComponentModel.DataAnnotations;

using NodaTime;

namespace Tatuaz.Shared.Domain.Models.Hist.Common;

public class HistEntity<TId>
    where TId : notnull
{
    public TId Id { get; set; } = default!;

    public Guid HistId { get; set; }

    public Instant HistFrom { get; set; }

    public Instant? HistTo { get; set; }
    public Instant Timestamp { get; set; }
}
