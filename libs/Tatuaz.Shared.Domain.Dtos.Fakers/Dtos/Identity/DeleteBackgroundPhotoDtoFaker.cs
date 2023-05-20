using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity;

public sealed class DeleteBackgroundPhotoDtoFaker : Faker<DeleteBackgroundPhotoDto>, IDtoFaker
{
    public DeleteBackgroundPhotoDtoFaker()
    {
        CustomInstantiator(f => new DeleteBackgroundPhotoDto());
    }
}
