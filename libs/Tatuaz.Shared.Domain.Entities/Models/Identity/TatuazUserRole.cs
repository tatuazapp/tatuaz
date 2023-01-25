using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Identity;

public class TatuazUserRole : Entity<HistTatuazUserRole, Guid>
{
    public string UserEmail { get; set; } = default!;
    public virtual TatuazUser User { get; set; } = default!;
    public Guid RoleId { get; set; }
    public virtual TatuazRole Role { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistTatuazUserRole)base.ToHistEntity(clock, state);
        histEntity.UserEmail = UserEmail;
        histEntity.RoleId = RoleId;
        return histEntity;
    }
}
