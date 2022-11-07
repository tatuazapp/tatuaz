using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Landing.ListArtistStats;

public sealed class EngagementStatDtoFaker : Faker<EngagementStatDto>
{
    public EngagementStatDtoFaker()
    {
        CustomInstantiator(f =>
            new EngagementStatDto(f.PickRandom<EngagementStatType>(),
                f.Lorem.Sentence()));
    }
}
