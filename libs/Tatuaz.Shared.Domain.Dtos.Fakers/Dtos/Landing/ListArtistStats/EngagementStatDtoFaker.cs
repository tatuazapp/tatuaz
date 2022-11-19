using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Landing.ListArtistStats;

public sealed class EngagementStatDtoFaker : Faker<EngagementStatDto>, IDtoFaker
{
    public EngagementStatDtoFaker()
    {
        CustomInstantiator(
            f => new EngagementStatDto(f.PickRandom<EngagementStatType>(), f.Lorem.Sentence())
        );
    }
}
