using System;

namespace Tatuaz.Dashboard.Queue.Contracts.Comment;

public record SubmitComment(Guid PostId, Guid? ParentCommentId, string Content);
