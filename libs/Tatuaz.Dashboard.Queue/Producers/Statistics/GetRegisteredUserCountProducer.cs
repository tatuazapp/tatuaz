using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Contracts.Statistics;

public class GetRegisteredUserCountProducer :
    TatuazProducerBase<GetRegisteredUserCountOrder, RegisteredUserCountDto>
{
    public GetRegisteredUserCountProducer(
        IRequestClient<GetRegisteredUserCountOrder> requestClient,
        ILogger<GetRegisteredUserCountProducer> logger
    )
        : base(requestClient, logger)
    {
    }
}
