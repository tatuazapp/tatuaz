using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Post;

public class GetUserPostsProducer : TatuazProducerBase<GetUserPosts, PagedData<BriefPostDto>>
{
    public GetUserPostsProducer(
        IRequestClient<GetUserPosts> requestClient,
        ILogger<GetUserPostsProducer> logger
    )
        : base(requestClient, logger) { }
}
