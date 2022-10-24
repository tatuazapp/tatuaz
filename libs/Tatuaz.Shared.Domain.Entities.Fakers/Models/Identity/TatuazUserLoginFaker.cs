using Bogus;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

public sealed class TatuazUserLoginFaker : Faker<TatuazUserLogin>
{
    public TatuazUserLoginFaker()
    {
        StrictMode(true);
        RuleFor(x => x.UserId, f => f.Random.Guid());
        RuleFor(x => x.LoginProvider, f => f.Internet.DomainName());
        RuleFor(x => x.ProviderKey, f => f.Random.Hash());
        RuleFor(x => x.ProviderDisplayName, f => f.Internet.UserName());
    }

    public TatuazUserLogin FromUserId(Guid userId)
    {
        var generated = Generate();
        generated.UserId = userId;
        return generated;
    }
}
