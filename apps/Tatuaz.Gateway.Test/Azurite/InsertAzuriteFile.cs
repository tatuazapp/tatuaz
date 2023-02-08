using System;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Xunit;

namespace Tatuaz.Gateway.Test.Azurite;

public class InsertAzuriteFile
{
    // for testing purposes with Azurite
    [Fact]
    public void InsertFile()
    {
        return; // comment this line to run the test
        var blobClient = new BlobServiceClient(
            "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;"
        );

        try
        {
            blobClient.CreateBlobContainer("tatuaz-images", PublicAccessType.Blob);
        }
        catch (Exception)
        {
            // ignored
        }

        var containerClient = blobClient.GetBlobContainerClient("tatuaz-images");
        var file = File.OpenRead(@"C:\Users\lukas\Downloads\test.jpg");
        containerClient.UploadBlob("test3.jpg", file);
    }
}
