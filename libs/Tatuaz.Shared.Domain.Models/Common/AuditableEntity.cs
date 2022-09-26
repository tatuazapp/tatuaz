using Tatuaz.Shared.Domain.Models.Hist.Common;

namespace Tatuaz.Shared.Domain.Models.Common;

public abstract class AuditableEntity<THistEntity, TId> : Entity<THistEntity, TId>, IAuditableEntity
    where THistEntity : AuditableHistEntity<TId>, new()
    where TId : notnull
{
    public Guid ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }

    public void UpdateCreationData(Guid userId)
    {
        CreatedOn = ModifiedOn = DateTime.UtcNow;
        CreatedBy = ModifiedBy = userId;
    }

    public void UpdateModificationData(Guid userId)
    {
        ModifiedOn = DateTime.UtcNow;
        ModifiedBy = userId;
    }

    public override THistEntity ToHistEntity()
    {
        var histEntity = base.ToHistEntity();
        histEntity.ModifiedBy = ModifiedBy;
        histEntity.ModifiedOn = ModifiedOn;
        histEntity.CreatedBy = CreatedBy;
        histEntity.CreatedOn = CreatedOn;
        return histEntity;
    }
}
