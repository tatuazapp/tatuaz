using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Post;

public sealed class CommentLikeFaker : Faker<CommentLike>, IEntityFaker
{
    public CommentLikeFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.CommentId, f => f.Random.Guid());
        RuleFor(x => x.Comment, _ => new CommentFaker().Generate());
        RuleFor(x => x.UserId, f => f.Random.Guid().ToString());
        RuleFor(x => x.User, _ => new TatuazUserFaker().Generate());
    }
}
