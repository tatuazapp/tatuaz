using System;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post;

public record FinalizePostDto(Guid InitialPostId, string Description, PhotoInfoDto[] PhotoInfoDtos)
    : IDto;
