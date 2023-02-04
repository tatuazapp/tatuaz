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
        RuleFor(x => x.UserEmail, f => f.Internet.Email());
        RuleFor(x => x.User, f => null!);
        RuleFor(x => x.RoleId, f => f.Random.Guid());
        RuleFor(x => x.Role, f => null!);
    }

    public TatuazUserRole FromUserIdAndRoleId(string tatuazUserId, Guid tatuazRoleId)
    {
        var generated = Generate();
        generated.UserEmail = tatuazUserId;
        generated.RoleId = tatuazRoleId;
        return generated;
    }

    public TatuazUserRole FromUserAndRole(TatuazUser tatuazUser, TatuazRole tatuazRole)
    {
        var generated = Generate();
        generated.User = tatuazUser;
        generated.UserEmail = tatuazUser.Id;
        generated.Role = tatuazRole;
        generated.RoleId = tatuazRole.Id;
        return generated;
    }
}
