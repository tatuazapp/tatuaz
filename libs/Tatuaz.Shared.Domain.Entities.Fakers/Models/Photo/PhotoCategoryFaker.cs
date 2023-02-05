using System.Collections.Generic;
using System.Linq;
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
        RuleFor(x => x.Title, f => f.Lorem.Words(3).Aggregate((a, b) => a + " " + b));
        RuleFor(x => x.Type, f => f.PickRandom<PhotoCategoryType>());
        RuleFor(x => x.ImageUrl, f => f.Internet.Url());
        RuleFor(x => x.Popularity, f => f.Random.Int(0, 100));
        RuleFor(x => x.UserPhotoCategories, _ => new List<UserPhotoCategory>());
    }
}
