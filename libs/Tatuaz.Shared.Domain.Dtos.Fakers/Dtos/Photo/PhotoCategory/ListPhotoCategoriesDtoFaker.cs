using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Photo.PhotoCategory;

public sealed class ListPhotoCategoriesDtoFaker : Faker<ListPhotoCategoriesDto>, IDtoFaker
{
    public ListPhotoCategoriesDtoFaker()
    {
        CustomInstantiator(
            f => new ListPhotoCategoriesDto(f.Random.Int(1, 100), f.Random.Int(1, 100))
        );
    }
}
