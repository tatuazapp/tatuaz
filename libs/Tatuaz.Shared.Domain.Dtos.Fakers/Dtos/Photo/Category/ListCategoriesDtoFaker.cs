using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Photo.Category;

public sealed class ListCategoriesDtoFaker : Faker<ListCategoriesDto>, IDtoFaker
{
    public ListCategoriesDtoFaker()
    {
        CustomInstantiator(f => new ListCategoriesDto(f.Random.Int(1, 100), f.Random.Int(1, 100)));
    }
}
