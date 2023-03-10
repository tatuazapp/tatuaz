using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Post;

public class FinalizePostProducer : TatuazProducerBase<FinalizePost, EmptyDto>
{
    public FinalizePostProducer(IRequestClient<FinalizePost> requestClient, ILogger<FinalizePostProducer> logger) : base(requestClient, logger)
    {
    }
}
