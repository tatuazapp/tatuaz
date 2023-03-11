using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Statistics;

public sealed class RegisteredStatsDtoFaker : Faker<RegisteredStatsDto>, IDtoFaker
{
    public RegisteredStatsDtoFaker()
    {
        CustomInstantiator(
            f => new RegisteredStatsDto(
                f.Random.Int(0, 10000),
                f.Random.Int(0, 1000),
                f.Random.Int(1000, 10000))
        );
    }

}
