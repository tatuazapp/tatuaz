using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Models.Booking;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Booking;

public record ListMyBookingRequestsDto(BookingRequestStatus? Status, int? PageNumber, int? PageSize)
    : IDto;
