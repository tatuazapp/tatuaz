using Microsoft.AspNetCore.Identity;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Identity;

public class TatuazUserToken : IdentityUserToken<Guid>, IHistDumpableEntity
{
    public HistEntity ToHistEntity(IClock clock, HistState state)
    {
        return new HistTatuazUserToken
        {
            HistDumpedAt = clock.GetCurrentInstant(),
            HistId = Guid.NewGuid(),
            HistState = state,
            UserId = UserId,
            LoginProvider = LoginProvider,
            Name = Name,
            Value = Value
        };
    }
}
