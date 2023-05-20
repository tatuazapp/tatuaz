using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post;

public record GetUserPostsDto(string? Username, int? PageNumber, int? PageSize) : IDto;
