using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Identity;

public static class DeleteForegroundPhotoErrorFactory
{
    public static TatuazError ForegroundPhotoNotFound(string? message = null)
    {
        return message is null
            ? new TatuazError(
                DeleteForegroundPhotoErrorCodes.ForegroundPhotoNotFound,
                "Foreground photo not found"
            )
            : new TatuazError(DeleteForegroundPhotoErrorCodes.ForegroundPhotoNotFound, message);
    }
}
