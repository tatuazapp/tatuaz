using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Post;

public static class LikePostResultFactory
{
    public static TatuazResult<T> PostNotFound<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { LikePostErrorFactory.PostNotFound(message) },
            httpStatusCode
        );
    }

    public static TatuazResult<T> PostAlreadyLiked<T>()
    {
        return new TatuazResult<T>(
            new[] { LikePostErrorFactory.PostAlreadyLiked() },
            HttpStatusCode.BadRequest
        );
    }

    public static TatuazResult<T> PostAlreadyUnliked<T>()
    {
        return new TatuazResult<T>(
            new[] { LikePostErrorFactory.PostAlreadyUnliked() },
            HttpStatusCode.BadRequest
        );
    }
}
