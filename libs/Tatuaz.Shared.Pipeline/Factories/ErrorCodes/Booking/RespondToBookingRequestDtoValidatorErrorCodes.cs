namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Booking;

public static class RespondToBookingRequestDtoValidatorErrorCodes
{
    public const string AcceptIsNull = "AcceptIsNull";
    public const string NotArtist = "NotArtist";
    public const string BookingRequestNotFound = "BookingRequestNotFound";
    public const string BookingRequestNotPending = "BookingRequestNotPending";
    public const string BookingRequestIdIsNull = "BookingRequestIdIsNull";
}
