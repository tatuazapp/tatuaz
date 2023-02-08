using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Photo.PhotoCategory;

public sealed class PhotoCategoryDtoFaker : Faker<PhotoCategoryDto>, IDtoFaker
{
    public PhotoCategoryDtoFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Int(1, 100));
        RuleFor(x => x.Title, f => f.Lorem.Word());
        RuleFor(x => x.Type, f => f.PickRandom<PhotoCategoryTypeDto>());
        RuleFor(x => x.ImageUrl, f => f.Internet.Url());
        RuleFor(x => x.Popularity, f => f.Random.Int(1, 100));
    }
}
