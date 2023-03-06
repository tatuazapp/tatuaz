using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Post;

public sealed class PostPhotoFaker : Faker<PostPhoto>, IEntityFaker
{
    public PostPhotoFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.PostId, f => f.Random.Guid());
        RuleFor(x => x.Post, f => new PostFaker().Generate());
        RuleFor(x => x.PhotoId, f => f.Random.Guid());
        RuleFor(x => x.Photo, f => new Photo.PhotoFaker().Generate());
    }
}
