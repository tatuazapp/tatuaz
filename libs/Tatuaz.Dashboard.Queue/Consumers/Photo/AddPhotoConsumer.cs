using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using SixLabors.ImageSharp;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Photo;

public class AddPhotoConsumer : TatuazConsumerBase<AddPhoto, Guid>
{
    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Photo.Photo,
        HistPhoto,
        Guid
    > _photoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly BlobContainerClient _blobContainerClient;
    private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

    public AddPhotoConsumer(
        ILogger<AddPhotoConsumer> logger,
        IGenericRepository<
            Shared.Domain.Entities.Models.Photo.Photo,
            HistPhoto,
            Guid
        > photoRepository,
        IUnitOfWork unitOfWork,
        BlobContainerClient blobContainerClient,
        RecyclableMemoryStreamManager recyclableMemoryStreamManager
    )
        : base(logger)
    {
        _photoRepository = photoRepository;
        _unitOfWork = unitOfWork;
        _blobContainerClient = blobContainerClient;
        _recyclableMemoryStreamManager = recyclableMemoryStreamManager;
    }

    protected override async Task<TatuazResult<Guid>> ConsumeMessage(
        ConsumeContext<AddPhoto> context
    )
    {
        var photo = new Shared.Domain.Entities.Models.Photo.Photo();
        await _unitOfWork
            .RunInTransactionAsync(
                async ct =>
                {
                    _photoRepository.Create(photo);
                    await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);

                    Image image;
                    using (
                        var stream = _recyclableMemoryStreamManager.GetStream(
                            "LoadImage",
                            context.Message.Data.Length
                        )
                    )
                    {
                        await stream.WriteAsync(context.Message.Data, ct).ConfigureAwait(false);
                        stream.Position = 0;
                        image = await Image.LoadAsync(stream, ct).ConfigureAwait(false);
                    }
                    using var jpgStream = _recyclableMemoryStreamManager.GetStream("SaveJpeg");
                    await image.SaveAsJpegAsync(jpgStream, ct).ConfigureAwait(false);
                    jpgStream.Position = 0;
                    var blobClient = _blobContainerClient.GetBlobClient($"{photo.Id:N}.jpg");
                    if (await blobClient.ExistsAsync(ct).ConfigureAwait(false))
                    {
                        throw new InvalidOperationException(
                            $"Blob with id {photo.Id} already exists"
                        );
                    }

                    await blobClient.UploadAsync(jpgStream, ct).ConfigureAwait(false);
                },
                cancellationToken: context.CancellationToken
            )
            .ConfigureAwait(false);

        return CommonResultFactory.Ok(photo.Id);
    }
}
