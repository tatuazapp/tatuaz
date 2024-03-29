using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;

public sealed class SignUpDtoFaker : Faker<SignUpDto>, IDtoFaker
{
    public SignUpDtoFaker()
    {
        CustomInstantiator(f => new SignUpDto(f.Random.Guid().ToString("N"), new[] { 1, 2, 3 }));
    }
}
