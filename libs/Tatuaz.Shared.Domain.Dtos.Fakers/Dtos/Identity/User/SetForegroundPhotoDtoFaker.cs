using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity.User;

public sealed class SetForegroundPhotoDtoFaker : Faker<SetForegroundPhotoDto>, IDtoFaker
{
    public SetForegroundPhotoDtoFaker()
    {
        CustomInstantiator(f => new SetForegroundPhotoDto(null));
    }
}
