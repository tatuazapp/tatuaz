using System;
using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Identity;

public class TatuazRole : Entity<HistTatuazRole, Guid>
{
    public string Name { get; set; } = default!;
    public virtual IEnumerable<TatuazUserRole> TatuazUserRoles { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistTatuazRole)base.ToHistEntity(clock, state);
        histEntity.Name = Name;
        return histEntity;
    }
}
