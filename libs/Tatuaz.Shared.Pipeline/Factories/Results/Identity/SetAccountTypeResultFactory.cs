using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Identity;

public static class SetAccountTypeResultFactory
{
    public static TatuazResult<T> AlreadyArtist<T>()
    {
        return new TatuazResult<T>(
            new[] { SetAccountTypeErrorFactory.AlreadyArtist() },
            HttpStatusCode.BadRequest
        );
    }

    public static TatuazResult<T> AlreadyClient<T>()
    {
        return new TatuazResult<T>(
            new[] { SetAccountTypeErrorFactory.AlreadyClient() },
            HttpStatusCode.BadRequest
        );
    }
}
