using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Identity;

public class DeleteBackgroundPhotoProducer : TatuazProducerBase<DeleteBackgroundPhoto, EmptyDto>
{
    public DeleteBackgroundPhotoProducer(
        IRequestClient<DeleteBackgroundPhoto> requestClient,
        ILogger<DeleteBackgroundPhotoProducer> logger
    )
        : base(requestClient, logger) { }
}
