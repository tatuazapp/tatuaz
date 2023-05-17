using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Post;

public class LikePostProducer : TatuazProducerBase<LikePost, EmptyDto>
{
    public LikePostProducer(
        IRequestClient<LikePost> requestClient,
        ILogger<LikePostProducer> logger
    )
        : base(requestClient, logger) { }
}
