using System;
using Bogus;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Post;

public sealed class FinalizePostDtoFaker : Faker<FinalizePostDto>, IDtoFaker
{
    public FinalizePostDtoFaker()
    {
        CustomInstantiator(
            f =>
                new FinalizePostDto(
                    f.Random.Guid(),
                    f.Lorem.Sentence(),
                    Array.Empty<PhotoInfoDto>()
                )
        );
    }
}
