using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

public class HistTatuazUserToken : HistEntity
{
    public Guid UserId { get; set; }
    public string LoginProvider { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Value { get; set; }
}
