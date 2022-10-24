using Bogus;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;

public sealed class GuidEntityFaker : Faker<Entity<HistEntity<Guid>, Guid>>
{
    public GuidEntityFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
    }
}
