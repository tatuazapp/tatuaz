using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Landing.ListSummaryStats;

public sealed class SummaryStatDtoFaker : Faker<SummaryStatDto>, IDtoFaker
{
    public SummaryStatDtoFaker()
    {
        CustomInstantiator(
            f => new SummaryStatDto(f.Lorem.Sentence(), f.Lorem.Word(), f.Internet.Url())
        );
    }
}
