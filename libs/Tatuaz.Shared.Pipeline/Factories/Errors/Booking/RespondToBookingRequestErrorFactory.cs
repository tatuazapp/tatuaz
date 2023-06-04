using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Booking;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Booking;

public static class RespondToBookingRequestErrorFactory
{
    public static TatuazError NotArtist(string? message = null)
    {
        return message is null
            ? new TatuazError(
                RespondToBookingRequestDtoValidatorErrorCodes.NotArtist,
                "Not an artist"
            )
            : new TatuazError(RespondToBookingRequestDtoValidatorErrorCodes.NotArtist, message);
    }

    public static TatuazError BookingRequestNotFound(string? message)
    {
        return message is null
            ? new TatuazError(
                RespondToBookingRequestDtoValidatorErrorCodes.BookingRequestNotFound,
                "Booking request not found"
            )
            : new TatuazError(
                RespondToBookingRequestDtoValidatorErrorCodes.BookingRequestNotFound,
                message
            );
    }

    public static TatuazError BookingRequestNotPending(string? message)
    {
        return message is null
            ? new TatuazError(
                RespondToBookingRequestDtoValidatorErrorCodes.BookingRequestNotPending,
                "Booking request not pending"
            )
            : new TatuazError(
                RespondToBookingRequestDtoValidatorErrorCodes.BookingRequestNotPending,
                message
            );
    }
}
