using Bogus;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

public sealed class TatuazUserFaker : Faker<TatuazUser>
{
    public TatuazUserFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Hash(20));
        RuleFor(x => x.Username, f => f.Internet.UserName());
        RuleFor(x => x.Email, f => f.Internet.Email());
        RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber());
        RuleFor(x => x.TatuazUserRoles, f => new List<TatuazUserRole>());
    }
}
