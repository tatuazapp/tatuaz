using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.Enums;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Landing;

public sealed class ListStatsDtoFaker : Faker<ListStatsDto>
{
    public ListStatsDtoFaker()
    {
        CustomInstantiator(f =>
            new ListStatsDto(f.PickRandom<StatsTimePeriod>(), f.Random.Int(1, 10)));
    }
}
