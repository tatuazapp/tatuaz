using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Booking;

public record ListMyBookingRequestsQuery(ListMyBookingRequestsDto ListMyBookingRequestsDto)
    : IRequest<TatuazResult<PagedData<BookingRequestDto>>>;
