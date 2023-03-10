using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Consumers.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Photo;

public class DeletePhotoProducer : TatuazProducerBase<DeletePhoto, EmptyDto>
{
    public DeletePhotoProducer(IRequestClient<DeletePhoto> requestClient, ILogger<DeletePhotoProducer> logger) : base(requestClient, logger)
    {
    }
}
