using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;

public sealed class CreateUserDtoFaker : Faker<CreateUserDto>, IDtoFaker
{
    public CreateUserDtoFaker()
    {
        CustomInstantiator(f => new CreateUserDto(f.Internet.UserName()));
    }
}
