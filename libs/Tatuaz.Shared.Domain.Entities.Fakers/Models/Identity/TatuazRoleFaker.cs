using System.Collections.Generic;
using Bogus;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

public sealed class TatuazRoleFaker : Faker<TatuazRole>
{
    public TatuazRoleFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.Name, f => f.Name.JobArea());
        RuleFor(x => x.TatuazUserRoles, f => new List<TatuazUserRole>());
    }
}