using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Identity;

public static class SetBackgroundPhotoErrorFactory
{
    public static TatuazError InvalidFileFormatError(string? message = null)
    {
        return message is null
            ? new TatuazError(
                SetBackgroundPhotoErrorCodes.InvalidFileFormat,
                "Invalid file format. Acceptable formats: jpg, jpeg, png, webp."
            )
            : new TatuazError(SetForegroundPhotoErrorCodes.InvalidFileFormat, message);
    }
}
