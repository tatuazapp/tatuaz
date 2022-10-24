using Bogus;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

public sealed class TatuazUserTokenFaker : Faker<TatuazUserToken>
{
    public TatuazUserTokenFaker()
    {
        StrictMode(true);
        RuleFor(x => x.UserId, f => f.Random.Guid());
        RuleFor(x => x.LoginProvider, f => f.Internet.DomainName());
        RuleFor(x => x.Name, f => f.Random.String(10));
        RuleFor(x => x.Value, f => f.Random.Hash());
    }

    public TatuazUserToken FromUserId(Guid userId)
    {
        var generated = Generate();
        generated.UserId = userId;
        return generated;
    }
}
