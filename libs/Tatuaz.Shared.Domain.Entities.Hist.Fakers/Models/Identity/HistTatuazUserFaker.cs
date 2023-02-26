using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Identity;

public sealed class HistTatuazUserFaker : Faker<HistTatuazUser>, IHistEntityFaker
{
    public HistTatuazUserFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Id, f => f.Internet.Email());
        RuleFor(x => x.Username, f => f.Internet.UserName());
        RuleFor(x => x.Auth0Id, f => f.Random.Guid().ToString());
        RuleFor(x => x.ForegroundPhotoId, f => f.Random.Guid());
        RuleFor(x => x.BackgroundPhotoId, f => f.Random.Guid());
    }
}
