using System;
using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Post;

public sealed class PhotoInfoDtoFaker : Faker<PhotoInfoDto>, IDtoFaker
{
    public PhotoInfoDtoFaker()
    {
        CustomInstantiator(f => new PhotoInfoDto(f.Random.Guid(), Array.Empty<int>()));
    }
}
