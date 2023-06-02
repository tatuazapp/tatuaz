using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Booking;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Booking;

public static class SendBookingRequestErrorFactory
{
    public static TatuazError ArtistNotFound(string? message = null)
    {
        return message is null
            ? new TatuazError(SendBookingRequestErrorCodes.ArtistNotFound, "Artist not found")
            : new TatuazError(SendBookingRequestErrorCodes.ArtistNotFound, message);
    }
}
