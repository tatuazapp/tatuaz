using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Booking;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Booking;

public static class RespondToBookingRequestErrorResultFactory
{
    public static TatuazResult<T> NotArtist<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { RespondToBookingRequestErrorFactory.NotArtist(message) },
            httpStatusCode
        );
    }

    public static TatuazResult<T> BookingRequestNotFound<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { RespondToBookingRequestErrorFactory.BookingRequestNotFound(message) },
            httpStatusCode
        );
    }

    public static TatuazResult<T> BookingRequestNotPending<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { RespondToBookingRequestErrorFactory.BookingRequestNotPending(message) },
            httpStatusCode
        );
    }
}
