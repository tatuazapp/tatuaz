using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;

public sealed class UserDtoFaker : Faker<UserDto>, IDtoFaker
{
    public UserDtoFaker()
    {
        CustomInstantiator(
            f => new UserDto(f.Internet.Email(), f.Internet.UserName(), f.Random.Guid().ToString())
        );
    }
}
