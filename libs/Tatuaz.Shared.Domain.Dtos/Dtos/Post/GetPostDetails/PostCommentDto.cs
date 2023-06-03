using System;
using NodaTime;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post.GetPostDetails;

public class PostCommentDto : IDto
{
    public Guid Id { get; init; }
    public Guid? ParentCommentId { get; init; }
    public string Content { get; init; }
    public string AuthorName { get; init; }
    public Uri? AuthorPhotoUri { get; init; }
    public int LikesCount { get; init; }
    public bool IsLikedByCurrentUser { get; init; }
    public Instant CreatedAt { get; init; }

    public PostCommentDto(
        Guid id,
        Guid? parentCommentId,
        string content,
        string authorName,
        Uri? authorPhotoUri,
        int likesCount,
        bool isLikedByCurrentUser,
        Instant createdAt
    )
    {
        Id = id;
        ParentCommentId = parentCommentId;
        Content = content;
        AuthorName = authorName;
        AuthorPhotoUri = authorPhotoUri;
        LikesCount = likesCount;
        IsLikedByCurrentUser = isLikedByCurrentUser;
        CreatedAt = createdAt;
    }
}
