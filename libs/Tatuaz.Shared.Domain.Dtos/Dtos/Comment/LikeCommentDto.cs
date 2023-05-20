using System;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Comment;

public record LikeCommentDto(Guid? CommentId, bool? Like);
