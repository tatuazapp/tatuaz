using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Photo;

public sealed class PhotoCategoryFaker : Faker<PhotoCategory>, IEntityFaker
{
    public PhotoCategoryFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Int(1, 100));
        RuleFor(x => x.PhotoId, f => f.Random.Guid());
        RuleFor(x => x.Photo, f => new PhotoFaker().Generate());
        RuleFor(x => x.CategoryId, f => f.Random.Int(1, 100));
        RuleFor(x => x.Category, f => new CategoryFaker().Generate());
    }
}
