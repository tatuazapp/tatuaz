using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Comment;

public class LikeCommentProducer : TatuazProducerBase<LikeComment, EmptyDto>
{
    public LikeCommentProducer(
        IRequestClient<LikeComment> requestClient,
        ILogger<LikeCommentProducer> logger) : base(requestClient, logger)
    {
    }
}
