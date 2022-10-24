using Bogus;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

public sealed class TatuazUserRoleFaker : Faker<TatuazUserRole>
{
    public TatuazUserRoleFaker()
    {
        StrictMode(true);
        RuleFor(x => x.UserId, f => f.Random.Guid());
        RuleFor(x => x.RoleId, f => f.Random.Guid());
    }

    public TatuazUserRole FromUserIdAndRoleId(Guid userId, Guid roleId)
    {
        return new TatuazUserRole
        {
            UserId = userId,
            RoleId = roleId
        };
    }
}
