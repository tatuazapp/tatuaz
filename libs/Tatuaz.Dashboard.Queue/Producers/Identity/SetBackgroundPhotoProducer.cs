using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Identity;

public class SetBackgroundPhotoProducer : TatuazProducerBase<SetBackgroundPhoto, EmptyDto>
{
    public SetBackgroundPhotoProducer(
        IRequestClient<SetBackgroundPhoto> requestClient,
        ILogger<SetBackgroundPhotoProducer> logger
    )
        : base(requestClient, logger) { }
}
