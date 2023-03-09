using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

public sealed class EmptyDtoFaker : Faker<EmptyDto>, IDtoFaker
{
    public EmptyDtoFaker()
    {
        StrictMode(true);
        CustomInstantiator(f => new EmptyDto());
    }
}
