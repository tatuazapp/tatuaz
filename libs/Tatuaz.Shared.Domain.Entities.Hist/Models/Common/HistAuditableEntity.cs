using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Attributes;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

[BaseHistEntity]
public class HistAuditableEntity<TId> : HistEntity<TId> where TId : notnull
{
    public Guid ModifiedBy { get; set; }
    public Instant ModifiedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Instant CreatedAt { get; set; }
}
