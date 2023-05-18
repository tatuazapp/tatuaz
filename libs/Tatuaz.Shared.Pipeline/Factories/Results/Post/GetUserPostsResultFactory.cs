using System.Net;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;
using Tatuaz.Shared.Pipeline.Factories.Errors.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Post;

public static class GetUserPostsResultFactory
{
    public static TatuazResult<T> UserNotFound<T>()
    {
        return new TatuazResult<T>(
            new[] { GetUserPostsErrorFactory.UserNotFound() },
            HttpStatusCode.BadRequest
        );
    }
}
