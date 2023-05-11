using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Post;

public class SearchPostsProducer : TatuazProducerBase<SearchPosts, PagedData<BriefPostDto>>
{
    public SearchPostsProducer(
        IRequestClient<SearchPosts> requestClient,
        ILogger<SearchPostsProducer> logger
    )
        : base(requestClient, logger) { }
}
