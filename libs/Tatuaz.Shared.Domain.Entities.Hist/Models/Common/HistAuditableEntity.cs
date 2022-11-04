using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Attributes;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

[BaseHistEntity]
public class HistAuditableEntity<TId> : HistEntity<TId> where TId : notnull
{
    public string ModifiedBy { get; set; } = default!;
    public Instant ModifiedAt { get; set; }
    public string CreatedBy { get; set; } = default!;
    public Instant CreatedAt { get; set; }
}