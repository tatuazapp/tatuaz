using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Post;

public class UploadPostPhotosProducer : TatuazProducerBase<UploadPostPhotos, UploadedPhotosDto>
{
    public UploadPostPhotosProducer(
        IRequestClient<UploadPostPhotos> requestClient,
        ILogger<UploadPostPhotosProducer> logger
    )
        : base(requestClient, logger) { }
}
