using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

public class HistTatuazUserRole : HistEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
