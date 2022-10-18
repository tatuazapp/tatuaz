using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.Shared.Domain.Entities.Common;

public abstract class AuditableEntity<THistEntity, TId> : Entity<THistEntity, TId>, IAuditableEntity
    where THistEntity : AuditableHistEntity<TId>, new()
    where TId : notnull
{
    public Guid ModifiedBy { get; set; }
    public Instant ModifiedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public Instant CreatedOn { get; set; }

    public void UpdateCreationData(Guid userId, IClock clock)
    {
        CreatedOn = ModifiedOn = clock.GetCurrentInstant();
        CreatedBy = ModifiedBy = userId;
    }

    public void UpdateModificationData(Guid userId, IClock clock)
    {
        ModifiedOn = clock.GetCurrentInstant();
        ModifiedBy = userId;
    }

    public override HistEntity<TId> ToHistEntity(IClock clock)
    {
        var histEntity = (AuditableHistEntity<TId>)base.ToHistEntity(clock);
        histEntity.ModifiedBy = ModifiedBy;
        histEntity.ModifiedOn = ModifiedOn;
        histEntity.CreatedBy = CreatedBy;
        histEntity.CreatedOn = CreatedOn;
        return histEntity;
    }
}