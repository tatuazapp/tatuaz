using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;

[TestIgnoreHistEntityFaker]
public sealed class IntHistAuditableEntityFaker : Faker<HistAuditableEntity<int>>, IHistEntityFaker
{
    public IntHistAuditableEntityFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Int(1, 100));
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.ModifiedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.ModifiedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.CreatedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.CreatedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
    }
}
