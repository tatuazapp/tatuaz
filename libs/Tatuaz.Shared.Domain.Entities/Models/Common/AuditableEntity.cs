using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Attributes;

namespace Tatuaz.Shared.Domain.Entities.Models.Common;

[BaseEntity]
public abstract class AuditableEntity<THistEntity, TId> : Entity<THistEntity, TId>, IAuditableEntity
    where THistEntity : HistAuditableEntity<TId>, new()
    where TId : notnull
{
    public string ModifiedBy { get; set; } = default!;
    public Instant ModifiedAt { get; set; }
    public string CreatedBy { get; set; } = default!;
    public Instant CreatedAt { get; set; }

    public void UpdateCreationData(string userId, IClock clock)
    {
        CreatedAt = ModifiedAt = clock.GetCurrentInstant();
        CreatedBy = ModifiedBy = userId;
    }

    public void UpdateModificationData(string userId, IClock clock)
    {
        ModifiedAt = clock.GetCurrentInstant();
        ModifiedBy = userId;
    }

    public override HistEntity<TId> ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistAuditableEntity<TId>)base.ToHistEntity(clock, state);
        histEntity.ModifiedBy = ModifiedBy;
        histEntity.ModifiedAt = ModifiedAt;
        histEntity.CreatedBy = CreatedBy;
        histEntity.CreatedAt = CreatedAt;
        return histEntity;
    }
}