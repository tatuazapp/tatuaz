using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Identity;

public sealed class HistTatuazUserFaker : Faker<HistTatuazUser>
{
    public HistTatuazUserFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Id, f => f.Random.Hash(20));
        RuleFor(x => x.Username, f => f.Internet.UserName());
        RuleFor(x => x.Email, f => f.Internet.Email());
        RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber());
    }
}