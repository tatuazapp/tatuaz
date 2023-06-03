using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post.GetPostDetails;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Post;

public class GetPostDetailsProducer : TatuazProducerBase<GetPostDetails, PostDetailsDto>
{
    public GetPostDetailsProducer(
        IRequestClient<GetPostDetails> requestClient,
        ILogger<GetPostDetails> logger
    )
        : base(requestClient, logger) { }
}
