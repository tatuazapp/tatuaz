using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Photo;

public class HistUserCategoryFaker : Faker<HistUserCategory>, IHistEntityFaker
{
    public HistUserCategoryFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Id, f => f.Random.Int(1, 100));
        RuleFor(x => x.UserId, f => f.Random.Guid().ToString());
        RuleFor(x => x.CategoryId, f => f.Random.Int(1, 100));
    }
}
