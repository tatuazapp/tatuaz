using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Booking;

public class ListIncomingBookingRequestsProducer
    : TatuazProducerBase<ListIncomingBookingRequests, PagedData<BookingRequestDto>>
{
    public ListIncomingBookingRequestsProducer(
        IRequestClient<ListIncomingBookingRequests> requestClient,
        ILogger<ListIncomingBookingRequestsProducer> logger
    )
        : base(requestClient, logger) { }
}
