using System;
using System.IO;
using SixLabors.ImageSharp;
using Tatuaz.Shared.Services.Common;
using Tatuaz.Shared.Services.Test.Fixtures;
using Xunit;

namespace Tatuaz.Shared.Services.Test.Common;

[Collection("ImageServiceCollection")]
public class ImageServiceTest
{
    private readonly ImageFixture _imageFixture;
    private readonly ImageService _imageService;
    private readonly string _tempPath;

    public ImageServiceTest(ImageFixture imageFixture)
    {
        _imageFixture = imageFixture;
        _imageService = imageFixture.ImageService;
        _tempPath = imageFixture.TempPath;
    }

    public class SaveImage : ImageServiceTest
    {
        public SaveImage(ImageFixture imageFixture) : base(imageFixture) { }

        [Fact]
        public void Should_SaveImage_WhenCorrectImageProvided()
        {
            using var fileStream = new FileStream(
                Path.Combine(_tempPath, "ryan.jpg"),
                FileMode.Open
            );
            var result = _imageService.SaveImage(fileStream, new Size(1024, 1024));

            Assert.True(File.Exists(Path.Combine(_tempPath, $"{result}.webp")));
        }

        [Fact]
        public void Should_CorrectlyResizeImage_WhenCorrectImageProvided()
        {
            using var fileStream = new FileStream(
                Path.Combine(_tempPath, "ryan.jpg"),
                FileMode.Open
            );
            var result = _imageService.SaveImage(fileStream, new Size(1024, 1024));

            using var image = Image.Load(Path.Combine(_tempPath, $"{result}.webp"));
            Assert.Equal(1024, image.Width);
            Assert.Equal(1024, image.Height);
        }

        [Fact]
        public void Should_ThrowException_WhenIncorrectImageProvided()
        {
            using var fileStream = new FileStream(
                Path.Combine(_tempPath, "test.jpg"),
                FileMode.Open
            );
            Assert.Throws<UnknownImageFormatException>(
                () => _imageService.SaveImage(fileStream, new Size(1024, 1024))
            );
        }

        [Fact]
        public void Should_ConvertImageToWebp_WhenCorrectImageProvided()
        {
            using var fileStream = new FileStream(
                Path.Combine(_tempPath, "ryan.jpg"),
                FileMode.Open
            );
            var result = _imageService.SaveImage(fileStream, new Size(1024, 1024));

            var (_, format) = Image
                .IdentifyWithFormatAsync(Path.Combine(_tempPath, $"{result}.webp"))
                .Result;
            Assert.Equal("Webp", format.Name);
        }
    }

    public class LoadImage : ImageServiceTest
    {
        public LoadImage(ImageFixture imageFixture) : base(imageFixture) { }

        [Fact]
        public void Should_LoadImage_WhenCorrectIdProvided()
        {
            using var fileStream = new FileStream(
                Path.Combine(_tempPath, "ryan.jpg"),
                FileMode.Open
            );
            var result = _imageService.SaveImage(fileStream, new Size(1024, 1024));

            var image = _imageService.LoadImage(result);

            Assert.NotNull(image);
        }

        [Fact]
        public void Should_ThrowException_WhenIncorrectIdProvided()
        {
            Assert.Throws<FileNotFoundException>(() => _imageService.LoadImage(Guid.NewGuid()));
        }
    }

    public class LoadImageStream : ImageServiceTest
    {
        public LoadImageStream(ImageFixture imageFixture) : base(imageFixture) { }

        [Fact]
        public void Should_LoadImage_WhenCorrectIdProvided()
        {
            using var fileStream = new FileStream(
                Path.Combine(_tempPath, "ryan.jpg"),
                FileMode.Open
            );
            var result = _imageService.SaveImage(fileStream, new Size(1024, 1024));

            var image = _imageService.LoadImageStream(result);

            Assert.NotNull(image);

            image.Close();
        }

        [Fact]
        public void Should_ThrowException_WhenIncorrectIdProvided()
        {
            Assert.Throws<FileNotFoundException>(
                () => _imageService.LoadImageStream(Guid.NewGuid())
            );
        }
    }
}
