using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Identity;

public sealed class HistTatuazUserTokenFaker : Faker<HistTatuazUserToken>
{
    public HistTatuazUserTokenFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.UserId, f => f.Random.Guid());
        RuleFor(x => x.LoginProvider, f => f.Internet.DomainName());
        RuleFor(x => x.Name, f => f.Random.String(10));
        RuleFor(x => x.Value, f => f.Random.Hash());
    }

    public HistTatuazUserToken FromUserId(Guid userId)
    {
        var generated = Generate();
        generated.UserId = userId;
        return generated;
    }
}
