using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;

[TestIgnoreHistEntityFaker]
public sealed class IntHistEntityFaker : Faker<HistEntity<int>>, IHistEntityFaker
{
    public IntHistEntityFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Int());
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
    }
}
