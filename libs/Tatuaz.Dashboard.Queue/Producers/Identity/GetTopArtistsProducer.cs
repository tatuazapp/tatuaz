using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Identity;

public class GetTopArtistsProducer : TatuazProducerBase<GetTopArtists, PagedData<BriefArtistDto>>
{
    public GetTopArtistsProducer(
        IRequestClient<GetTopArtists> requestClient,
        ILogger<GetTopArtistsProducer> logger
    )
        : base(requestClient, logger) { }
}
