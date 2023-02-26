using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Photo.Category;

public sealed class PhotoCategoryDtoFaker : Faker<CategoryDto>, IDtoFaker
{
    public PhotoCategoryDtoFaker()
    {
        CustomInstantiator(
            f =>
                new CategoryDto(
                    f.Random.Int(1, 100),
                    f.Lorem.Word(),
                    f.PickRandom<CategoryTypeDto>(),
                    f.Internet.Url()
                )
        );
    }
}
