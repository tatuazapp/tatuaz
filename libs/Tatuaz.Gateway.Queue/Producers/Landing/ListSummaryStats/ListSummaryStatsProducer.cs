using System.Collections.Generic;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Gateway.Queue.Contracts;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Gateway.Queue.Producers;

public class ListSummaryStatsProducer
    : TatuazProducerBase<ListSummaryStatsOrder, IEnumerable<SummaryStatDto>>
{
    public ListSummaryStatsProducer(
        IRequestClient<ListSummaryStatsOrder> requestClient,
        IUserAccessor userAccessor,
        ILogger<ListSummaryStatsProducer> logger
    ) : base(requestClient, userAccessor, logger) { }
}