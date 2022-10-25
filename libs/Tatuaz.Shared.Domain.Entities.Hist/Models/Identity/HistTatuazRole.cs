using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

public class HistTatuazRole : HistEntity<Guid>
{
    public string Name { get; set; } = default!;
}
