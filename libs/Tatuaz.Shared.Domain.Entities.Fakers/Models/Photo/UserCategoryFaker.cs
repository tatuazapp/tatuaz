using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Photo;

public sealed class UserCategoryFaker : Faker<UserCategory>, IEntityFaker
{
    public UserCategoryFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Int(1, 100));
        RuleFor(x => x.UserId, f => f.Random.Guid().ToString());
        RuleFor(x => x.User, f => new TatuazUserFaker().Generate());
        RuleFor(x => x.CategoryId, f => f.Random.Int(1, 100));
        RuleFor(x => x.Category, f => new CategoryFaker().Generate());
    }
}
