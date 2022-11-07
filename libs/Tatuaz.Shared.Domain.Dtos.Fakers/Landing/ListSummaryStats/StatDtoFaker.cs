using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Landing.ListSummaryStats;

public sealed class SummaryStatDtoFaker : Faker<SummaryStatDto>
{
    public SummaryStatDtoFaker()
    {
        CustomInstantiator(f => new SummaryStatDto(f.Lorem.Sentence(), f.Lorem.Word(), f.Internet.Url()));
    }
}
