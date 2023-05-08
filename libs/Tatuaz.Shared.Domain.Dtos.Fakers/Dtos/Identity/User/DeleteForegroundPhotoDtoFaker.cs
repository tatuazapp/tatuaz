using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Identity.User;

public sealed class DeleteForegroundPhotoDtoFaker : Faker<DeleteForegroundPhotoDto>, IDtoFaker
{
    public DeleteForegroundPhotoDtoFaker()
    {
        CustomInstantiator(f => new DeleteForegroundPhotoDto());
    }
}
