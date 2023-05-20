using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Comment;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Comment;

public static class LikeCommentErrorFactory
{
    public static TatuazError CommentNotFound(string? message = null)
    {
        return message is null
            ? new TatuazError(LikeCommentErrorCodes.CommentNotFound, "Comment not found")
            : new TatuazError(LikeCommentErrorCodes.CommentNotFound, message);
    }

    public static TatuazError CommentAlreadyLiked()
    {
        return new TatuazError(LikeCommentErrorCodes.CommentAlreadyLiked, "Comment already liked");
    }

    public static TatuazError CommentAlreadyUnliked()
    {
        return new TatuazError(LikeCommentErrorCodes.CommentAlreadyUnliked, "Comment already unliked");
    }
}
