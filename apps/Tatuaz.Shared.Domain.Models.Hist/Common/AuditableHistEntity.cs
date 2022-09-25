namespace Tatuaz.Shared.Domain.Models.Hist.Common;

public class AuditableHistEntity<TId> : HistEntity<TId>
    where TId : notnull
{
    public Guid ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}
