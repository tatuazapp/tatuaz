using Bogus;
using NetTopologySuite.Geometries;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.General;

public sealed class HistCityFaker : Faker<HistCity>, IHistEntityFaker
{
    public HistCityFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.Name, f => f.Address.City());
        RuleFor(x => x.TimeZoneId, f => f.Random.Guid());
        RuleFor(x => x.Country, f => f.Address.Country());
        RuleFor(
            x => x.Location,
            f => new Point(f.Random.Double(-90, 90), f.Random.Double(-180, 180))
        );
    }
}
