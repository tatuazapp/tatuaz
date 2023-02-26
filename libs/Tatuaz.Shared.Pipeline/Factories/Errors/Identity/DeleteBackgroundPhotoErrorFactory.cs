using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Identity;

public class DeleteBackgroundPhotoErrorFactory
{
    public static TatuazError BackgroundPhotoNotFound(string? message = null)
    {
        return message is null
            ? new TatuazError(
                DeleteBackgroundPhotoErrorCodes.BackgroundPhotoNotFound,
                "Background photo not found"
            )
            : new TatuazError(DeleteBackgroundPhotoErrorCodes.BackgroundPhotoNotFound, message);
    }
}
