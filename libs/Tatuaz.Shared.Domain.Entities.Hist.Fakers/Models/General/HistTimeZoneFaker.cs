using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.General;

public sealed class HistTimeZoneFaker : Faker<HistTimeZone>, IHistEntityFaker
{
    public HistTimeZoneFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.Name, f => "CET");
        RuleFor(x => x.OffsetFromUtc, f => f.Random.Int(-12, 12));
        RuleFor(x => x.Description, f => f.Lorem.Sentence());
    }
}
