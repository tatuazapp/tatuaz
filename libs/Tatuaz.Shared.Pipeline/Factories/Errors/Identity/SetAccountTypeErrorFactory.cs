using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Identity;

public static class SetAccountTypeErrorFactory
{
    public static TatuazError AlreadyArtist(string? message = null)
    {
        return new TatuazError(
            SetAccountTypeErrorCodes.AlreadyArtist,
            message ?? "User is already an artist."
        );
    }

    public static TatuazError AlreadyClient(string? message = null)
    {
        return new TatuazError(
            SetAccountTypeErrorCodes.AlreadyClient,
            message ?? "User is already a client."
        );
    }
}
