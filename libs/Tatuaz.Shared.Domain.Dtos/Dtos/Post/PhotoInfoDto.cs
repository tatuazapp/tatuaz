using System;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post;

public record PhotoInfoDto(Guid PhotoId, int[] CategoryIds) : IDto
{
    public string PhotoFileName => $"{PhotoId:N}.jpg";
}
