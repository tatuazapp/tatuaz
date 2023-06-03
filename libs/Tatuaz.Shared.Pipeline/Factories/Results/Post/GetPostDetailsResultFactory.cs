using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Post;

public static class GetPostDetailsResultFactory
{
    public static TatuazResult<T> PostDoesNotExist<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { GetPostDetailsErrorFactory.PostDoesNotExist(message) },
            httpStatusCode
        );
    }
}
