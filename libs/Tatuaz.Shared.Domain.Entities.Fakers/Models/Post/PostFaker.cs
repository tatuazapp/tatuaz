using System.Collections.Generic;
using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Post;

public sealed class PostFaker : Faker<Entities.Models.Post.Post>, IEntityFaker
{
    public PostFaker()
    {
        StrictMode(true);
        RuleFor(x => x.ModifiedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.ModifiedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.CreatedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.CreatedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.AuthorId, f => f.Random.Guid().ToString());
        RuleFor(x => x.Author, f => new TatuazUserFaker().Generate());
        RuleFor(x => x.Description, f => f.Lorem.Paragraph());
        RuleFor(x => x.Photos, _ => new List<Entities.Models.Post.PostPhoto>());
        RuleFor(x => x.Likes, _ => new List<Entities.Models.Post.PostLike>());
        RuleFor(x => x.Comments, _ => new List<Entities.Models.Post.Comment>());
    }
}
