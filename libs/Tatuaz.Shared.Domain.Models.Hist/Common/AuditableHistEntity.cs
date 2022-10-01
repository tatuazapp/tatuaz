using NodaTime;

namespace Tatuaz.Shared.Domain.Models.Hist.Common;

public class AuditableHistEntity<TId> : HistEntity<TId>
    where TId : notnull
{
    public Guid ModifiedBy { get; set; }
    public Instant ModifiedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public Instant CreatedOn { get; set; }
}
