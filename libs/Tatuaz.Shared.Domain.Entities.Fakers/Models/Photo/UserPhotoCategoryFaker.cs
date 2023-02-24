using Bogus;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Photo;

public sealed class UserPhotoCategoryFaker : Faker<UserCategory>, IEntityFaker
{
    public UserPhotoCategoryFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.UserId, f => f.Random.Guid().ToString());
        RuleFor(x => x.User, f => new TatuazUserFaker().Generate());
        RuleFor(x => x.PhotoCategoryId, f => f.Random.Int(1, 100));
        RuleFor(x => x.Category, f => new CategoryFaker().Generate());
    }
}
