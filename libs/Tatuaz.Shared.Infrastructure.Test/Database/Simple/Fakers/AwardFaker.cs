using System;
using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.Fakers;

public sealed class AwardFaker : Faker<Award>
{
    public AwardFaker()
    {
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.CreatedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.CreatedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.ModifiedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.ModifiedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Name, f => f.Lorem.Word());
        RuleFor(x => x.BookId, f => f.Random.Guid());
    }

    public Award FromBookId(Guid bookId)
    {
        var award = Generate();
        award.BookId = bookId;
        return award;
    }
}
