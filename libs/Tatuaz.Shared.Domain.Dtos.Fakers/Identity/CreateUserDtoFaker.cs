using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Identity;

public sealed class CreateUserDtoFaker : Faker<CreateUserDto>
{
    public CreateUserDtoFaker()
    {
        CustomInstantiator(f =>
            new CreateUserDto(f.Internet.UserName(), f.Internet.Email(), "1" + f.Phone.PhoneNumber("#########")));
    }
}