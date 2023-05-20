using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Comment;

public class SubmitCommentProducer : TatuazProducerBase<SubmitComment, SubmittedCommentDto>
{
    public SubmitCommentProducer(
        IRequestClient<SubmitComment> requestClient,
        ILogger<SubmitCommentProducer> logger
    )
        : base(requestClient, logger) { }
}
