using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;

public sealed class SetBioDtoFaker : Faker<SetBioDto>, IDtoFaker
{
    public SetBioDtoFaker()
    {
        CustomInstantiator(f => new SetBioDto(f.Lorem.Sentence(), f.Address.City()));
    }
}
