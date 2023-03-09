using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Blob;

namespace Tatuaz.Dashboard.Queue.Consumers.Blob;

public class DeleteBlobFileConsumer : IConsumer<DeleteBlobFile>
{
    private readonly BlobContainerClient _blobContainerClient;

    public DeleteBlobFileConsumer(BlobContainerClient blobContainerClient)
    {
        _blobContainerClient = blobContainerClient;
    }

    public async Task Consume(ConsumeContext<DeleteBlobFile> context)
    {
        var blobClient = _blobContainerClient.GetBlobClient($"{context.Message.Id:N}.jpg");
        if (await blobClient.ExistsAsync().ConfigureAwait(false))
        {
            await blobClient.DeleteAsync().ConfigureAwait(false);
        }
        else
        {
            throw new InvalidOperationException(
                $"Blob with id {context.Message.Id} does not exist"
            );
        }
    }
}
