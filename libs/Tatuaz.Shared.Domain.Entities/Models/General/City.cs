using System;
using NetTopologySuite.Geometries;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.General;

public class City : Entity<HistCity, Guid>, IEntity
{
    public string Name { get; set; } = default!;
    public virtual TimeZone TimeZone { get; set; } = default!;
    public Guid TimeZoneId { get; set; }
    public string Country = default!;
    public Point Location { get; set; } = default!;

    public override HistCity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistCity)base.ToHistEntity(clock, state);
        histEntity.Name = Name;
        histEntity.TimeZoneId = TimeZoneId;
        histEntity.Country = Country;
        histEntity.Location = Location;
        return histEntity;
    }
}
