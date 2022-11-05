using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Identity;

public sealed class UserDtoFaker : Faker<UserDto>
{
    public UserDtoFaker()
    {
        CustomInstantiator(f => new UserDto(f.Internet.Email(), f.Internet.UserName()));
    }
}
