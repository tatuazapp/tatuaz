using System;
using NetTopologySuite.Geometries;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.General;

public class HistCity : HistEntity<Guid>, IHistEntity
{
    public string Name { get; set; } = default!;
    public Guid TimeZoneId { get; set; }
    public string Country = default!;
    public Point Location { get; set; } = default!;
}
