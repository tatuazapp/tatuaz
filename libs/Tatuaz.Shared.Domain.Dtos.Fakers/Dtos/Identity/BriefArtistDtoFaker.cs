using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;

public sealed class BriefArtistDtoFaker : Faker<BriefUserDto>, IDtoFaker
{
    public BriefArtistDtoFaker()
    {
        CustomInstantiator(f => new BriefUserDto(f.Internet.UserName(), null, null, null, null));
    }
}
