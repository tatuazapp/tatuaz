using System;

namespace Tatuaz.Dashboard.Queue.Contracts.Comment;

public record LikeComment(Guid CommentId, bool Like);
