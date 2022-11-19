using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.General;

public class HistTimeZone : HistEntity<Guid>, IHistEntity
{
    public string Name { get; set; } = default!;
    public int OffsetFromUtc { get; set; }
    public string Description { get; set; } = default!;
}
