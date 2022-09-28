﻿using NodaTime;

using Tatuaz.Shared.Domain.Models.Hist.Common;

namespace Tatuaz.Shared.Domain.Models.Common;

public abstract class Entity<THistEntity, TId>
    where THistEntity : HistEntity<TId>, new()
    where TId : notnull
{
    public TId Id { get; set; } = default!;
    public Instant Timestamp { get; set; }

    public virtual THistEntity ToHistEntity(IClock clock)
    {
        var histEntity = new THistEntity {
            Id = Id,
            HistFrom = clock.GetCurrentInstant(),
            HistTo = null
        };
        return histEntity;
    }
}
