using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Tatuaz.Shared.Helpers;
using Tatuaz.Shared.Services.Common;
using Tatuaz.Shared.Services.Options;
using Xunit;

namespace Tatuaz.Shared.Services.Test.Fixtures;

public class ImageFixture : IDisposable
{
    public ImageService ImageService { get; private set; }
    public string TempPath { get; private set; }

    public ImageFixture()
    {
        TempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");
        ImageService = new ImageService(
            new OptionsWrapper<ImageOptions>(new ImageOptions() { BaseDirectory = TempPath })
        );

        if (!Directory.Exists(TempPath))
        {
            Directory.CreateDirectory(TempPath);
        }

        SeedImagesDirectory();
    }

    public void Dispose()
    {
        Directory.Delete(TempPath, true);
    }

    private void SeedImagesDirectory()
    {
        DownloadFile(
            "https://wallpaperaccess.com/full/1351202.jpg",
            Path.Combine(TempPath, "ryan.jpg")
        );

        File.Create(Path.Combine(TempPath, "test.jpg")).Close();
    }

    private static void DownloadFile(string url, string filePath)
    {
        using var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var bytes = response.Content.ReadAsStream().FullyReadStream();
        var file = File.Create(filePath);
        file.Write(bytes, 0, bytes.Length);
        file.Close();
    }
}
