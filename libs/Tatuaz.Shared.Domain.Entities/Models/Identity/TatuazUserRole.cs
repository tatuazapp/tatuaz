using Microsoft.AspNetCore.Identity;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Identity;

public class TatuazUserRole : IdentityUserRole<Guid>, IHistDumpableEntity
{
    public HistEntity ToHistEntity(IClock clock, HistState state)
    {
        return new HistTatuazUserRole
        {
            HistDumpedAt = clock.GetCurrentInstant(),
            HistId = Guid.NewGuid(),
            HistState = state,
            UserId = UserId,
            RoleId = RoleId
        };
    }
}
