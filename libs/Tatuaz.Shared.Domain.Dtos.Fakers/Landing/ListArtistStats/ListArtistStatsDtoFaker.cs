using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Landing.ListArtistStats;

public class ListArtistStatsDtoFaker : Faker<ListArtistStatsDto>
{
    public ListArtistStatsDtoFaker()
    {
        CustomInstantiator(f => new ListArtistStatsDto(f.Random.Int(1, 10)));
    }
}
