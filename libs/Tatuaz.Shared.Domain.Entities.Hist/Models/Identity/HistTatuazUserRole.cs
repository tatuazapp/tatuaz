using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

public class HistTatuazUserRole : HistEntity<Guid>
{
    public string UserEmail { get; set; } = default!;
    public Guid RoleId { get; set; }
}
