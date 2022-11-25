using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

public class HistTatuazUserRole : HistEntity<Guid>, IHistEntity
{
    public string TatuazUserId { get; set; } = default!;
    public Guid TatuazRoleId { get; set; }
}
