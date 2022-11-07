using System.Collections.Generic;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Gateway.Queue.Contracts;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Gateway.Queue.Producers;

public class ListStatsProducer : TatuazProducerBase<ListStatsOrder, IEnumerable<SummaryStatDto>>
{
    public ListStatsProducer(IRequestClient<ListStatsOrder> requestClient, IUserAccessor userAccessor,
        ILogger<ListStatsProducer> logger) : base(
        requestClient, userAccessor, logger)
    {
    }
}
