using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

public class HistTatuazUser : HistEntity<string>
{
    public string Username { get; set; } = default!;
    public string Auth0Id { get; set; } = default!;
}
