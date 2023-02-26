using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Identity;

public static class DeleteForegroundPhotoResultFactory
{
    public static TatuazResult<T> ForegroundPhotoNotFound<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { DeleteForegroundPhotoErrorFactory.ForegroundPhotoNotFound(message) },
            httpStatusCode
        );
    }
}
