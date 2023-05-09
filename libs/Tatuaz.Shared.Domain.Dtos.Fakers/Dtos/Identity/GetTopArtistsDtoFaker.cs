using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;

public class GetTopArtistsDtoFaker : Faker<GetTopArtistsDto>, IDtoFaker
{
    public GetTopArtistsDtoFaker()
    {
        CustomInstantiator(f => new GetTopArtistsDto(1,1));
    }
}
