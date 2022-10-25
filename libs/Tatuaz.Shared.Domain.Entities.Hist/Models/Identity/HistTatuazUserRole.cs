using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

public class HistTatuazUserRole : HistEntity<Guid>
{
    public Guid TatuazUserId { get; set; }
    public Guid TatuazRoleId { get; set; }
}
