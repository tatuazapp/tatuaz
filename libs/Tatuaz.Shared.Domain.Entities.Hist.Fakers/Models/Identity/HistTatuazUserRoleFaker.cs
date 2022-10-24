using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Identity;

public sealed class HistTatuazUserRoleFaker : Faker<HistTatuazUserRole>
{
    public HistTatuazUserRoleFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.UserId, f => f.Random.Guid());
        RuleFor(x => x.RoleId, f => f.Random.Guid());
    }

    public HistTatuazUserRole FromUserIdAndRoleId(Guid userId, Guid roleId)
    {
        var generated = Generate();
        generated.UserId = userId;
        generated.RoleId = roleId;
        return generated;
    }
}
