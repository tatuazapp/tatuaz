using Bogus;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

public sealed class TatuazRoleClaimFaker : Faker<TatuazRoleClaim>
{
    public TatuazRoleClaimFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Int(1, 100));
        RuleFor(x => x.RoleId, f => f.Random.Guid());
        RuleFor(x => x.ClaimType, f => f.Random.String(10));
        RuleFor(x => x.ClaimValue, f => f.Random.String(10));
    }

    public TatuazRoleClaim FromRoleId(Guid roleId)
    {
        var generated = Generate();
        generated.RoleId = roleId;
        return generated;
    }
}
