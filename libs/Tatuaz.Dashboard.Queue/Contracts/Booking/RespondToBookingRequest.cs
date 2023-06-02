namespace Tatuaz.Dashboard.Queue.Contracts.Booking;

public record RespondToBookingRequest(int BookingRequestId, bool Accept);
