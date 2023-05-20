using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Comment;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Comment;

public static class LikeCommentResultFactory
{
    public static TatuazResult<T> CommentNotFound<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { LikeCommentErrorFactory.CommentNotFound(message) },
            httpStatusCode
        );
    }

    public static TatuazResult<T> CommentAlreadyLiked<T>()
    {
        return new TatuazResult<T>(
            new[] { LikeCommentErrorFactory.CommentAlreadyLiked() },
            HttpStatusCode.BadRequest
        );
    }

    public static TatuazResult<T> CommentAlreadyUnliked<T>()
    {
        return new TatuazResult<T>(
            new[] { LikeCommentErrorFactory.CommentAlreadyUnliked() },
            HttpStatusCode.BadRequest
        );
    }
}
