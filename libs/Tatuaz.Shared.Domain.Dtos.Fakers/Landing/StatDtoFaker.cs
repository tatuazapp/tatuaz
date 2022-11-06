using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Landing;

public sealed class StatDtoFaker : Faker<StatDto>
{
    public StatDtoFaker()
    {
        CustomInstantiator(f => new StatDto(f.Lorem.Sentence(), f.Lorem.Word(), f.Internet.Url()));
    }
}
