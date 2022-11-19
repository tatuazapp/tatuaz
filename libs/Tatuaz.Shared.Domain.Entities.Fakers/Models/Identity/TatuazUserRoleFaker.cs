using System;
using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

public sealed class TatuazUserRoleFaker : Faker<TatuazUserRole>, IEntityFaker
{
    public TatuazUserRoleFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.TatuazUserId, f => f.Random.Hash(20));
        RuleFor(x => x.TatuazUser, f => null!);
        RuleFor(x => x.TatuazRoleId, f => f.Random.Guid());
        RuleFor(x => x.TatuazRole, f => null!);
    }

    public TatuazUserRole FromUserIdAndRoleId(string tatuazUserId, Guid tatuazRoleId)
    {
        var generated = Generate();
        generated.TatuazUserId = tatuazUserId;
        generated.TatuazRoleId = tatuazRoleId;
        return generated;
    }

    public TatuazUserRole FromUserAndRole(TatuazUser tatuazUser, TatuazRole tatuazRole)
    {
        var generated = Generate();
        generated.TatuazUser = tatuazUser;
        generated.TatuazUserId = tatuazUser.Id;
        generated.TatuazRole = tatuazRole;
        generated.TatuazRoleId = tatuazRole.Id;
        return generated;
    }
}
