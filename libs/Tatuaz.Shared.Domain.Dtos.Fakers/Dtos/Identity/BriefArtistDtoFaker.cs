using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;

public sealed class BriefArtistDtoFaker : Faker<BriefArtistDto>, IDtoFaker
{
    public BriefArtistDtoFaker()
    {
        CustomInstantiator(f => new BriefArtistDto(f.Internet.UserName(), null, null, null, null));
    }
}
