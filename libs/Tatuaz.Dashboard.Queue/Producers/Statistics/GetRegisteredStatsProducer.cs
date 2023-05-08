using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Statistics;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Statistics;

public class GetRegisteredStatsProducer : TatuazProducerBase<GetRegisteredStats, RegisteredStatsDto>
{
    public GetRegisteredStatsProducer(
        IRequestClient<GetRegisteredStats> requestClient,
        ILogger<GetRegisteredStatsProducer> logger
    )
        : base(requestClient, logger) { }
}
