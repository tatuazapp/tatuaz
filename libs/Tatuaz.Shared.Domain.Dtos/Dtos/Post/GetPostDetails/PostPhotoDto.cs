using System;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post.GetPostDetails;

public class PostPhotoDto : IDto
{
    public Uri Uri { get; init; }
    public int[] CategoryIds { get; init; }

    public PostPhotoDto(Uri uri, int[] categoryIds)
    {
        Uri = uri;
        CategoryIds = categoryIds;
    }
}
