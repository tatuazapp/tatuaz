using Bogus;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;

public sealed class StringEntityFaker : Faker<Entity<HistEntity<string>, string>>
{
    public StringEntityFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid().ToString());
    }
}
