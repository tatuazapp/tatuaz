using Bogus;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

public sealed class TatuazUserClaimFaker : Faker<TatuazUserClaim>
{
    public TatuazUserClaimFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Int(1, 100));
        RuleFor(x => x.UserId, f => f.Random.Guid());
        RuleFor(x => x.ClaimType, f => f.Random.String(10));
        RuleFor(x => x.ClaimValue, f => f.Random.String(10));
    }

    public TatuazUserClaim FromUserId(Guid userId)
    {
        var generated = Generate();
        generated.UserId = userId;
        return generated;
    }
}
