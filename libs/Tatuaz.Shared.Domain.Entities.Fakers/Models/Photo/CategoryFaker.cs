using System.Collections.Generic;
using System.Linq;
using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Photo;

public sealed class CategoryFaker : Faker<Category>, IEntityFaker
{
    public CategoryFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Int(1, 100));
        RuleFor(x => x.Title, f => f.Lorem.Words(3).Aggregate((a, b) => a + " " + b));
        RuleFor(x => x.Type, f => f.PickRandom<CategoryType>());
        RuleFor(x => x.ImageUri, f => f.Internet.UrlRootedPath("jpg"));
        RuleFor(x => x.UserCategories, _ => new List<UserCategory>());
    }
}
