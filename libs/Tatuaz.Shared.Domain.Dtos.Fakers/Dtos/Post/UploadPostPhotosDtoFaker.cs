using System;
using Bogus;
using Microsoft.AspNetCore.Http;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Post;

public sealed class UploadPostPhotosDtoFaker : Faker<UploadPostPhotosDto>, IDtoFaker
{
    public UploadPostPhotosDtoFaker()
    {
        CustomInstantiator(f => new UploadPostPhotosDto(Array.Empty<IFormFile>()));
    }
}
