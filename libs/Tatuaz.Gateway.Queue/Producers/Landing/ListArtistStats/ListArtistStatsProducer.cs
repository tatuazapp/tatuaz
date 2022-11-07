using System.Collections.Generic;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Gateway.Queue.Contracts.Landing.ListArtistStats;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Gateway.Queue.Producers.Landing.ListArtistStats;

public class ListArtistStatsProducer
    : TatuazProducerBase<ListArtistStatsOrder, IEnumerable<ArtistStatDto>>
{
    public ListArtistStatsProducer(
        IRequestClient<ListArtistStatsOrder> requestClient,
        IUserAccessor userAccessor,
        ILogger<ListArtistStatsProducer> logger
    ) : base(requestClient, userAccessor, logger) { }
}
