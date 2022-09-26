using Tatuaz.Shared.Domain.Models.Hist.Common;

namespace Tatuaz.Shared.Domain.Models.Common;

public abstract class Entity<THistEntity, TId>
    where THistEntity : HistEntity<TId>, new()
    where TId : notnull
{
    public TId Id { get; set; } = default!;

    public virtual THistEntity ToHistEntity()
    {
        var histEntity = new THistEntity {
            Id = Id,
            HistFrom = DateTime.UtcNow,
            HistTo = null
        };
        return histEntity;
    }
}
