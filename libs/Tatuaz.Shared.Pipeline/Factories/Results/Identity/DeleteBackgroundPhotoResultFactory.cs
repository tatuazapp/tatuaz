using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Identity;

public static class DeleteBackgroundPhotoResultFactory
{
    public static TatuazResult<T> BackgroundPhotoNotFound<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { DeleteBackgroundPhotoErrorFactory.BackgroundPhotoNotFound(message) },
            httpStatusCode
        );
    }
}
