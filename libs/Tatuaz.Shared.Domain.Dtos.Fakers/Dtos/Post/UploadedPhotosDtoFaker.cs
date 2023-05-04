using System;
using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Post;

public sealed class UploadedPhotosDtoFaker : Faker<UploadedPhotosDto>, IDtoFaker
{
    public UploadedPhotosDtoFaker()
    {
        CustomInstantiator(f => new UploadedPhotosDto(f.Random.Guid(), Array.Empty<Guid>()));
    }
}
