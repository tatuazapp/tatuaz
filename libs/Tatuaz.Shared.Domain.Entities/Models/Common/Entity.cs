using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Attributes;

namespace Tatuaz.Shared.Domain.Entities.Models.Common;

[BaseEntity]
public abstract class Entity<THistEntity, TId> : IHistDumpableEntity
    where THistEntity : HistEntity<TId>, new()
    where TId : notnull
{
    public TId Id { get; set; } = default!;

    public virtual HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = new THistEntity
        {
            Id = Id,
            HistDumpedAt = clock.GetCurrentInstant(),
            HistId = Guid.NewGuid(),
            HistState = state
        };
        return histEntity;
    }
}