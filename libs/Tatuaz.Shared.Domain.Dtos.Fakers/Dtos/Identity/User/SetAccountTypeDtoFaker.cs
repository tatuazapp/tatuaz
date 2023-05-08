using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity.User;

public sealed class SetAccountTypeDtoFaker : Faker<SetAccountTypeDto>, IDtoFaker
{
    public SetAccountTypeDtoFaker()
    {
        CustomInstantiator(f => new SetAccountTypeDto(f.Random.Bool()));
    }
}
