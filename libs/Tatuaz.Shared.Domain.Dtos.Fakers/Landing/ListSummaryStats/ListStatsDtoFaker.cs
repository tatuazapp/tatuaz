using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Landing.ListSummaryStats;

public sealed class ListSummaryStatsDtoFaker : Faker<ListSummaryStatsDto>
{
    public ListSummaryStatsDtoFaker()
    {
        CustomInstantiator(
            f => new ListSummaryStatsDto(f.PickRandom<SummaryStatTimePeriod>(), f.Random.Int(1, 10))
        );
    }
}
