using System;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Comment;

public record SubmittedCommentDto(Guid CommentId, string Content) : IDto;
