using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using MassTransit;
using SixLabors.ImageSharp;
using Tatuaz.Dashboard.Queue.Contracts.Blob;

namespace Tatuaz.Dashboard.Queue.Consumers.Blob;

public class AddBlobFileConsumer : IConsumer<AddBlobFile>
{
    private readonly BlobContainerClient _blobContainerClient;

    public AddBlobFileConsumer(BlobContainerClient blobContainerClient)
    {
        _blobContainerClient = blobContainerClient;
    }

    public async Task Consume(ConsumeContext<AddBlobFile> context)
    {
        var stream = new MemoryStream(context.Message.Data);
        var image = await Image.LoadAsync(stream).ConfigureAwait(false);
        var jpgStream = new MemoryStream();
        await image.SaveAsJpegAsync(jpgStream).ConfigureAwait(false);
        jpgStream.Position = 0;
        var blobClient = _blobContainerClient.GetBlobClient($"{context.Message.Id:N}.jpg");
        if (await blobClient.ExistsAsync().ConfigureAwait(false))
        {
            throw new InvalidOperationException(
                $"Blob with id {context.Message.Id} already exists"
            );
        }
        await blobClient.UploadAsync(jpgStream).ConfigureAwait(false);
    }
}
