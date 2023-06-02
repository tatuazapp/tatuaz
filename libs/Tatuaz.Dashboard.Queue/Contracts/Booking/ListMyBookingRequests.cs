using Tatuaz.Shared.Domain.Entities.Models.Booking;

namespace Tatuaz.Dashboard.Queue.Contracts.Booking;

public record ListMyBookingRequests(int PageSize, int PageNumber, BookingRequestStatus Status);
