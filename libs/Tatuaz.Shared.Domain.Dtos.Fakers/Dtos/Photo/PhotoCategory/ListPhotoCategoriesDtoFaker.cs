using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Photo.PhotoCategory;

public sealed class ListPhotoCategoriesDtoFaker : Faker<ListPhotoCategoriesDto>, IDtoFaker
{
    public ListPhotoCategoriesDtoFaker()
    {
        StrictMode(true);
        RuleFor(x => x.PageNumber, f => f.Random.Int(1, 100));
        RuleFor(x => x.PageSize, f => f.Random.Int(1, 100));
    }
}
