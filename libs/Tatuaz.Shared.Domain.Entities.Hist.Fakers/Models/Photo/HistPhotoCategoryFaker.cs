using System.Linq;
using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Photo;

public sealed class HistPhotoCategoryFaker : Faker<HistCategory>, IHistEntityFaker
{
    public HistPhotoCategoryFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Id, f => f.Random.Int(1, 100));
        RuleFor(x => x.Title, f => f.Lorem.Words(3).Aggregate((a, b) => a + " " + b));
        RuleFor(x => x.Type, f => f.PickRandom<HistCategoryType>());
        RuleFor(x => x.ImageUri, f => f.Internet.UrlRootedPath("jpg"));
        RuleFor(x => x.Popularity, f => f.Random.Int(0, 100));
    }
}
