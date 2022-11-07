using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Landing.ListArtistStats;

public class ArtistStatDtoFaker : Faker<ArtistStatDto>
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
