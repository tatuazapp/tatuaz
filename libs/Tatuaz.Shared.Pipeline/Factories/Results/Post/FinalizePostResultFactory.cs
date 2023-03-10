using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Post;

public static class FinalizePostResultFactory
{
    public static TatuazResult<T> InitialPostDoesNotExist<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { FinalizePostErrorFactory.InitialPostDoesNotExist(message) },
            httpStatusCode
        );
    }

    public static TatuazResult<T> UserIsNotTheAuthorOfTheInitialPost<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { FinalizePostErrorFactory.UserIsNotTheAuthorOfTheInitialPost(message) },
            httpStatusCode
        );
    }
}
