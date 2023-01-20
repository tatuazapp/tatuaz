using System;
using System.IO;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using Tatuaz.Shared.Services.Options;

namespace Tatuaz.Shared.Services.Common;

public class ImageService : IImageService
{
    private readonly string _imageBaseDirectory;

    public ImageService(IOptions<ImageOptions> imageOptions)
    {
        _imageBaseDirectory = imageOptions.Value.BaseDirectory;
    }

    public Guid SaveImage(Stream stream, Size size)
    {
        using var image = Image.Load(stream);
        image.Mutate(x => x.Resize(size));
        var guid = Guid.NewGuid();
        var path = Path.Combine(_imageBaseDirectory, guid.ToString());
        image.Save($"{path}.webp", new WebpEncoder());
        return guid;
    }

    public Image LoadImage(Guid id)
    {
        var path = Path.Combine(_imageBaseDirectory, id.ToString());
        return Image.Load($"{path}.webp");
    }

    public Stream LoadImageStream(Guid id)
    {
        var path = Path.Combine(_imageBaseDirectory, id.ToString());
        return File.OpenRead($"{path}.webp");
    }
}
