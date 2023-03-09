using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Identity;

public class SetForegroundPhotoProducer : TatuazProducerBase<SetForegroundPhoto, EmptyDto>
{
    public SetForegroundPhotoProducer(
        IRequestClient<SetForegroundPhoto> requestClient,
        ILogger<SetForegroundPhotoProducer> logger
    )
        : base(requestClient, logger) { }
}
