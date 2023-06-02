using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Booking;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Booking;

public static class ListIncomingBookingRequestsError
{
    public static TatuazError NotArtist(string? message = null)
    {
        return message is null
            ? new TatuazError(ListIncomingBookingRequestsErrorCodes.NotArtist, "Artist not found")
            : new TatuazError(ListIncomingBookingRequestsErrorCodes.NotArtist, message);
    }
}
