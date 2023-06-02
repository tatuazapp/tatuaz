using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Booking;

public class ListMyBookingRequestsProducer
    : TatuazProducerBase<ListMyBookingRequests, PagedData<BookingRequestDto>>
{
    public ListMyBookingRequestsProducer(
        IRequestClient<ListMyBookingRequests> requestClient,
        ILogger<ListMyBookingRequestsProducer> logger
    )
        : base(requestClient, logger) { }
}
