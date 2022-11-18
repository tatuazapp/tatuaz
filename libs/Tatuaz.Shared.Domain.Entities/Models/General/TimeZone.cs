using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.General;

public class TimeZone : Entity<HistTimeZone, Guid>
{
    public string Name { get; set; } = default!;
    public int OffsetFromUtc { get; set; }
    public string Description { get; set; } = default!;

    public override HistTimeZone ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistTimeZone)base.ToHistEntity(clock, state);
        histEntity.Name = Name;
        histEntity.OffsetFromUtc = OffsetFromUtc;
        histEntity.Description = Description;
        return histEntity;
    }
}
