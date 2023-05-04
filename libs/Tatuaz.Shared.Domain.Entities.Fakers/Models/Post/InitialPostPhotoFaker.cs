using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Post;

public sealed class InitialPostPhotoFaker : Faker<InitialPostPhoto>, IEntityFaker
{
    public InitialPostPhotoFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.InitialPostId, f => f.Random.Guid());
        RuleFor(x => x.InitialPost, _ => new InitialPostFaker().Generate());
        RuleFor(x => x.PhotoId, f => f.Random.Guid());
        RuleFor(x => x.Photo, _ => new PhotoFaker().Generate());
    }
}
