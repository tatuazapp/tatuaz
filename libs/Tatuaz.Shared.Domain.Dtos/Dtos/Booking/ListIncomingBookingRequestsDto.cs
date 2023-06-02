using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Models.Booking;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Booking;

public record ListIncomingBookingRequestsDto(
    BookingRequestStatus? Status,
    int? PageNumber,
    int? PageSize
) : IDto;
