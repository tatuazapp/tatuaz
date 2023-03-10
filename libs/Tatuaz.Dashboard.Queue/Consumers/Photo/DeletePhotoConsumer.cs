using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Photo;

public class DeletePhotoConsumer : TatuazConsumerBase<DeletePhoto, EmptyDto>
{
    private readonly IGenericRepository<Shared.Domain.Entities.Models.Photo.Photo, HistPhoto, Guid> _photoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly BlobContainerClient _blobContainerClient;

    public DeletePhotoConsumer(
        ILogger<DeletePhotoConsumer> logger,
        IGenericRepository<Shared.Domain.Entities.Models.Photo.Photo, HistPhoto, Guid> photoRepository,
        IUnitOfWork unitOfWork,
        BlobContainerClient blobContainerClient
        ) : base(logger)
    {
        _photoRepository = photoRepository;
        _unitOfWork = unitOfWork;
        _blobContainerClient = blobContainerClient;
    }

    protected override async Task<TatuazResult<EmptyDto>> ConsumeMessage(ConsumeContext<DeletePhoto> context)
    {
        await _unitOfWork.RunInTransactionAsync(async ct =>
        {
            await _photoRepository.DeleteAsync(context.Message.Id, ct).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);
            var blobClient = _blobContainerClient.GetBlobClient($"{context.Message.Id:N}.jpg");
            if (await blobClient.ExistsAsync(ct).ConfigureAwait(false))
            {
                await blobClient.DeleteAsync(cancellationToken: ct).ConfigureAwait(false);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Blob with id {context.Message.Id} does not exist"
                );
            }
        }, cancellationToken: context.CancellationToken).ConfigureAwait(false);

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
