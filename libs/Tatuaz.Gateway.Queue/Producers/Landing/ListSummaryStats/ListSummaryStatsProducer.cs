using System.Collections.Generic;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Gateway.Queue.Contracts.Landing.ListSummaryStats;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Gateway.Queue.Producers.Landing.ListSummaryStats;

public class ListSummaryStatsProducer
    : TatuazProducerBase<ListSummaryStatsOrder, IEnumerable<SummaryStatDto>>
{
    public ListSummaryStatsProducer(
        IRequestClient<ListSummaryStatsOrder> requestClient,
        ILogger<ListSummaryStatsProducer> logger
    ) : base(requestClient, logger) { }
}
