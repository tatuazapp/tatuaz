using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Post;

public sealed class HistPostPhotoFaker : Faker<HistPostPhoto>, IHistEntityFaker
{
    public HistPostPhotoFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.PostId, f => f.Random.Guid());
        RuleFor(x => x.PhotoId, f => f.Random.Guid());
    }
}
