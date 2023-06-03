using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Booking;

public class RespondToBookingRequestProducer : TatuazProducerBase<RespondToBookingRequest, EmptyDto>
{
    public RespondToBookingRequestProducer(
        IRequestClient<RespondToBookingRequest> requestClient,
        ILogger<RespondToBookingRequestProducer> logger
    )
        : base(requestClient, logger) { }
}
