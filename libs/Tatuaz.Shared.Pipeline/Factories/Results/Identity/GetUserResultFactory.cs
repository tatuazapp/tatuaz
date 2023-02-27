using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Identity;

public static class GetUserResultFactory
{
    public static TatuazResult<T> UserNotFound<T>(string username)
    {
        return new TatuazResult<T>(
            new[] { GetUserErrorFactory.UserNotFound(username) },
            HttpStatusCode.NotFound
        );
    }
}
