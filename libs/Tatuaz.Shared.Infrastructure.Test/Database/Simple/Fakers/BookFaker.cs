using System;
using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.Fakers;

public sealed class BookFaker : Faker<Book>
{
    public BookFaker()
    {
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.CreatedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.CreatedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.ModifiedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.ModifiedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Title, f => f.Lorem.Sentence());
        RuleFor(x => x.AuthorId, f => f.Random.Guid());
    }

    public Book FromAuthorId(Guid authorId)
    {
        var book = Generate();
        book.AuthorId = authorId;
        return book;
    }
}
