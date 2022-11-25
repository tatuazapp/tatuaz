using System;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Identity;

public class TatuazUserRole : Entity<HistTatuazUserRole, Guid>, IEntity
{
    public string TatuazUserId { get; set; } = default!;
    public virtual TatuazUser TatuazUser { get; set; } = default!;
    public Guid TatuazRoleId { get; set; }
    public virtual TatuazRole TatuazRole { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistTatuazUserRole)base.ToHistEntity(clock, state);
        histEntity.TatuazUserId = TatuazUserId;
        histEntity.TatuazRoleId = TatuazRoleId;
        return histEntity;
    }
}
