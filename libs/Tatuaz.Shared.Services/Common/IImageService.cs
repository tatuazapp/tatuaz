using System;
using System.IO;
using SixLabors.ImageSharp;

namespace Tatuaz.Shared.Services.Common;

public interface IImageService
{
    Guid SaveImage(Stream stream, Size size);
    Image LoadImage(Guid id);
    Stream LoadImageStream(Guid id);
}
