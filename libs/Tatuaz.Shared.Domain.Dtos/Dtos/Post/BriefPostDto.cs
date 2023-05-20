using System;
using NodaTime;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post;

public class BriefPostDto : IDto
{
    public Guid Id { get; init; }
    public string Description { get; init; }
    public Uri[] PhotoUris { get; init; }
    public string AuthorName { get; init; }
    public Uri? AuthorPhotoUri { get; init; }
    public int LikesCount { get; init; }
    public bool IsLikedByCurrentUser { get; set; }
    public int CommentsCount { get; init; }
    public Instant CreatedAt { get; init; }

    public BriefPostDto(
        Guid id,
        string description,
        Uri[] photoUris,
        string authorName,
        Uri? authorPhotoUri,
        int likesCount,
        bool isLikedByCurrentUser,
        int commentsCount,
        Instant createdAt
    )
    {
        Id = id;
        Description = description;
        PhotoUris = photoUris;
        AuthorName = authorName;
        AuthorPhotoUri = authorPhotoUri;
        LikesCount = likesCount;
        IsLikedByCurrentUser = isLikedByCurrentUser;
        CommentsCount = commentsCount;
        CreatedAt = createdAt;
    }
}
