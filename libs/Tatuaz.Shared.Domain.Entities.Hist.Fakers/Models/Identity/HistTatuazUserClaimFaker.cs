using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Identity;

public sealed class HistTatuazUserClaimFaker : Faker<HistTatuazUserClaim>
{
    public HistTatuazUserClaimFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Id, f => f.Random.Int(1, 1000));
        RuleFor(x => x.UserId, f => f.Random.Guid());
        RuleFor(x => x.ClaimType, f => f.Random.String(10));
        RuleFor(x => x.ClaimValue, f => f.Random.String(10));
    }

    public HistTatuazUserClaim FromUserId(Guid userId)
    {
        var generated = Generate();
        generated.UserId = userId;
        return generated;
    }
}
