using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.General;

public sealed class EmailInfoFaker : Faker<EmailInfo>, IEntityFaker
{
    public EmailInfoFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.RecipientEmail, f => f.Internet.Email());
        RuleFor(x => x.EmailType, f => f.Name.JobArea());
        RuleFor(x => x.ObjectId, f => f.Random.Guid());
        RuleFor(x => x.OrderedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.SentAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.RetryCount, f => f.Random.Int(0, 10));
        RuleFor(x => x.Error, f => f.Lorem.Sentence());
    }
}
