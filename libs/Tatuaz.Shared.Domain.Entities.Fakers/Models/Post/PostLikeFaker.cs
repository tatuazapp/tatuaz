using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Post;

public sealed class PostLikeFaker : Faker<PostLike>, IEntityFaker
{
    public PostLikeFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.PostId, f => f.Random.Guid());
        RuleFor(x => x.Post, f => new PostFaker().Generate());
        RuleFor(x => x.UserId, f => f.Random.Guid().ToString());
        RuleFor(x => x.User, f => new TatuazUserFaker().Generate());
    }
}
