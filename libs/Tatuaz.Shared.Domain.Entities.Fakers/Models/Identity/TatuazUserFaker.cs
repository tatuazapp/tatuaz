using System.Collections.Generic;
using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

public sealed class TatuazUserFaker : Faker<TatuazUser>, IEntityFaker
{
    public TatuazUserFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Internet.Email());
        RuleFor(x => x.Username, f => f.Internet.UserName());
        RuleFor(x => x.Auth0Id, f => f.Random.Guid().ToString());
        RuleFor(x => x.UserRoles, _ => new List<TatuazUserRole>());
    }
}
