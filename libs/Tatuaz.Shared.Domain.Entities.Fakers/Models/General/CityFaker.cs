using Bogus;
using NetTopologySuite.Geometries;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.General;

public sealed class CityFaker : Faker<City>, IEntityFaker
{
    public CityFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.Name, f => f.Address.City());
        RuleFor(x => x.TimeZone, _ => new TimeZoneFaker().Generate());
        RuleFor(x => x.TimeZoneId, f => f.Random.Guid());
        RuleFor(x => x.Country, f => f.Address.Country());
        RuleFor(
            x => x.Location,
            f => new Point(f.Random.Double(-90, 90), f.Random.Double(-180, 180))
        );
    }
}
