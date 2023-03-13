using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Contracts.Statistics;

public class GetRegisteredStatsProducer : TatuazProducerBase<GetRegisteredStats, RegisteredStatsDto>
{
    public GetRegisteredStatsProducer(
        IRequestClient<GetRegisteredStats> requestClient,
        ILogger<GetRegisteredStatsProducer> logger
    )
        : base(requestClient, logger) { }
}
