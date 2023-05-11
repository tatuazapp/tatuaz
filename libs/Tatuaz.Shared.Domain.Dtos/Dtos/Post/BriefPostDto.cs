using System;
using NodaTime;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post;

public record BriefPostDto(
    Guid Id,
    string Description,
    Uri[] PhotoUris,
    string AuthorName,
    Uri? AuthorPhotoUri,
    int LikesCount,
    int CommentsCount,
    Instant CreatedAt
) : IDto;
