using System;
using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post.GetPostDetails;

public class PostDetailsDto : IDto
{
    public Guid Id { get; init; }
    public string Description { get; init; }
    public string AuthorName { get; init; }
    public Uri? AuthorPhotoUri { get; init; }
    public int LikesCount { get; init; }
    public bool IsLikedByCurrentUser { get; set; }
    public Instant CreatedAt { get; init; }
    public ICollection<PostCommentDto> ParentComments { get; init; }
    public ICollection<PostPhotoDto> Photos { get; set; }

    public PostDetailsDto(
        Guid id,
        string description,
        string authorName,
        Uri? authorPhotoUri,
        int likesCount,
        bool isLikedByCurrentUser,
        Instant createdAt,
        ICollection<PostCommentDto> comments
    )
    {
        Id = id;
        Description = description;
        AuthorName = authorName;
        AuthorPhotoUri = authorPhotoUri;
        LikesCount = likesCount;
        IsLikedByCurrentUser = isLikedByCurrentUser;
        CreatedAt = createdAt;
        ParentComments = comments;
    }

    public PostDetailsDto() { }
}
