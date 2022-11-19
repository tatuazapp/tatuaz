using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Landing.ListArtistStats;

public class ListArtistStatsDtoFaker : Faker<ListArtistStatsDto>, IDtoFaker
{
    public ListArtistStatsDtoFaker()
    {
        CustomInstantiator(f => new ListArtistStatsDto(f.Random.Int(1, 10)));
    }
}
