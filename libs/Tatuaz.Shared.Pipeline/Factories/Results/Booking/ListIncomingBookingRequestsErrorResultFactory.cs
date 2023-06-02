using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Booking;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Booking;

public static class ListIncomingBookingRequestsErrorResultFactory
{
    public static TatuazResult<T> NotArtist<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { ListIncomingBookingRequestsError.NotArtist(message) },
            httpStatusCode
        );
    }
}
