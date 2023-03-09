using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Contracts.Statistics;

public class GetRegisteredStatsProducer :
    TatuazProducerBase<GetRegisteredStatsOrder, RegisteredStatsDto>
{
    public GetRegisteredStatsProducer(
        IRequestClient<GetRegisteredStatsOrder> requestClient,
        ILogger<GetRegisteredStatsProducer> logger
    )
        : base(requestClient, logger)
    {
    }
}
