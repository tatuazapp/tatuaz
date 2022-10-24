using Bogus;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

public sealed class TatuazUserFaker : Faker<TatuazUser>
{
    public TatuazUserFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.UserName, f => f.Internet.UserName());
        RuleFor(x => x.NormalizedUserName, f => f.Internet.UserName().ToUpper());
        RuleFor(x => x.Email, f => f.Internet.Email());
        RuleFor(x => x.NormalizedEmail, f => f.Internet.Email().ToUpper());
        RuleFor(x => x.EmailConfirmed, f => f.Random.Bool());
        RuleFor(x => x.PasswordHash, f => f.Random.Hash());
        RuleFor(x => x.SecurityStamp, f => f.Random.Hash());
        RuleFor(x => x.ConcurrencyStamp, f => f.Random.Hash());
        RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber());
        RuleFor(x => x.PhoneNumberConfirmed, f => f.Random.Bool());
        RuleFor(x => x.TwoFactorEnabled, f => f.Random.Bool());
        RuleFor(x => x.LockoutEnd, f => f.Date.Future());
        RuleFor(x => x.LockoutEnabled, f => f.Random.Bool());
        RuleFor(x => x.AccessFailedCount, f => f.Random.Int(0, 100));
    }
}
