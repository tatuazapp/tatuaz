using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;

[TestIgnoreEntityFaker]
public sealed class IntEntityFaker : Faker<Entity<HistEntity<int>, int>>, IEntityFaker
{
    public IntEntityFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Int(1, 100));
    }
}
