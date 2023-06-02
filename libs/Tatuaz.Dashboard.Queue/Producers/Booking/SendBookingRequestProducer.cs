using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Booking;

public class SendBookingRequestProducer : TatuazProducerBase<SendBookingRequest, EmptyDto>
{
    public SendBookingRequestProducer(
        IRequestClient<SendBookingRequest> requestClient,
        ILogger<SendBookingRequestProducer> logger
    )
        : base(requestClient, logger) { }
}
