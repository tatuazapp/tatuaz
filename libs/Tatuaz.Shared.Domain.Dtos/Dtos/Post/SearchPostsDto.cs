using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post;

public record SearchPostsDto(
    string? Query,
    int? PageNumber,
    int? PageSize,
    SearchPostsFlag? SearchPostsFlag
) : IDto;
