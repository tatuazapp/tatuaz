using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Landing.ListArtistStats;

public class ArtistStatDtoFaker : Faker<ArtistStatDto>, IDtoFaker
{
    public ArtistStatDtoFaker()
    {
        CustomInstantiator(
            f =>
                new ArtistStatDto(
                    f.Internet.UserName(),
                    f.Internet.Url(),
                    f.Internet.Url(),
                    new EngagementStatDtoFaker().Generate()
                )
        );
    }
}
