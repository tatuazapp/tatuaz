using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.General;

public sealed class TimeZoneFaker : Faker<TimeZone>, IEntityFaker
{
    public TimeZoneFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.Name, f => "CET");
        RuleFor(x => x.OffsetFromUtc, f => f.Random.Int(-12, 12));
        RuleFor(x => x.Description, f => f.Lorem.Sentence());
    }
}
