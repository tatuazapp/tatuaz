using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Landing.ListSummaryStats;

public sealed class ListSummaryStatsDtoFaker : Faker<ListSummaryStatsDto>, IDtoFaker
{
    public ListSummaryStatsDtoFaker()
    {
        CustomInstantiator(
            f => new ListSummaryStatsDto(f.PickRandom<SummaryStatTimePeriod>(), f.Random.Int(1, 10))
        );
    }
}
