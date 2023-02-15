using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Photo.PhotoCategory;

public sealed class PhotoCategoryDtoFaker : Faker<PhotoCategoryDto>, IDtoFaker
{
    public PhotoCategoryDtoFaker()
    {
        CustomInstantiator(
            f =>
                new PhotoCategoryDto(
                    f.Random.Int(1, 100),
                    f.Lorem.Word(),
                    f.PickRandom<PhotoCategoryTypeDto>(),
                    f.Internet.Url(),
                    f.Random.Int(1, 100)
                )
        );
    }
}
