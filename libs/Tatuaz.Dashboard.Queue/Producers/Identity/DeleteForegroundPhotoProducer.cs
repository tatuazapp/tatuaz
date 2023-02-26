using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Identity;

public class DeleteForegroundPhotoProducer : TatuazProducerBase<DeleteForegroundPhoto, EmptyDto>
{
    public DeleteForegroundPhotoProducer(
        IRequestClient<DeleteForegroundPhoto> requestClient,
        ILogger<DeleteForegroundPhotoProducer> logger
    )
        : base(requestClient, logger) { }
}
