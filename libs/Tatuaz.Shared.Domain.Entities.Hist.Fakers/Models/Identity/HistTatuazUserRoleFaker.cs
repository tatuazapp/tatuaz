using System;
using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Identity;

public sealed class HistTatuazUserRoleFaker : Faker<HistTatuazUserRole>, IHistEntityFaker
{
    public HistTatuazUserRoleFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.UserEmail, f => f.Internet.Email());
        RuleFor(x => x.RoleId, f => f.Random.Guid());
    }

    public HistTatuazUserRole FromUserIdAndRoleId(string tatuazUserId, Guid tatuazRoleId)
    {
        var generated = Generate();
        generated.UserEmail = tatuazUserId;
        generated.RoleId = tatuazRoleId;
        return generated;
    }
}
