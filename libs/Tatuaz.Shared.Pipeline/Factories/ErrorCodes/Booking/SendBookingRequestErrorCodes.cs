namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Booking;

public static class SendBookingRequestErrorCodes
{
    public const string ArtistNameIsNull = "ArtistEmailIsNull";
    public const string StartIsNull = "StartIsNull";
    public const string StartIsGreaterThanEnd = "StartIsGreaterThanEnd";
    public const string EndIsNull = "EndIsNull";
    public const string CommentIsTooLong = "CommentIsTooLong";
    public const string ArtistNotFound = "ArtistNotFound";
}
