using System;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Consumers.Photo;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Producers.Photo;

public class AddPhotoProducer : TatuazProducerBase<AddPhoto, Guid>
{
    public AddPhotoProducer(IRequestClient<AddPhoto> requestClient, ILogger<AddPhotoProducer> logger) : base(requestClient, logger)
    {
    }
}
