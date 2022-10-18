using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.Shared.Domain.Entities.Common;

public abstract class Entity<THistEntity, TId> : IHistDumpableEntity
    where THistEntity : HistEntity<TId>, new()
    where TId : notnull
{
    public TId Id { get; set; } = default!;

    public virtual HistEntity ToHistEntity(IClock clock)
    {
        var histEntity = new THistEntity { Id = Id, HistDumpedAt = clock.GetCurrentInstant() };
        return histEntity;
    }
}