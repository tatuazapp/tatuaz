using System.Collections.Generic;
using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Post;

public sealed class CommentFaker : Faker<Comment>, IEntityFaker
{
    public CommentFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.ModifiedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.ModifiedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.CreatedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.CreatedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.ParentCommentId, f => f.Random.Guid());
        RuleFor(x => x.ParentComment, _ => null);
        RuleFor(x => x.ChildComments, _ => new List<Comment>());
        RuleFor(x => x.PostId, f => f.Random.Guid());
        RuleFor(x => x.Post, f => new PostFaker().Generate());
        RuleFor(x => x.UserId, f => f.Random.Guid().ToString());
        RuleFor(x => x.User, f => new TatuazUserFaker().Generate());
        RuleFor(x => x.Content, f => f.Lorem.Paragraph());
    }
}
