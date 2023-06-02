using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Booking;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Booking;

public static class SendBookingRequestResultFactory
{
    public static TatuazResult<T> ArtistNotFound<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { SendBookingRequestErrorFactory.ArtistNotFound(message) },
            httpStatusCode
        );
    }
}
