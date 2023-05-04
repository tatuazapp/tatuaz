using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Post;

public static class FinalizePostErrorFactory
{
    public static TatuazError InitialPostDoesNotExist(string? message = null)
    {
        return message is null
            ? new TatuazError(
                FinalizePostErrorCodes.InitialPostDoesNotExist,
                "Initial post does not exist"
            )
            : new TatuazError(FinalizePostErrorCodes.InitialPostDoesNotExist, message);
    }

    public static TatuazError UserIsNotTheAuthorOfTheInitialPost(string? message = null)
    {
        return message is null
            ? new TatuazError(
                FinalizePostErrorCodes.UserIsNotTheAuthorOfTheInitialPost,
                "User is not the author of the initial post"
            )
            : new TatuazError(FinalizePostErrorCodes.UserIsNotTheAuthorOfTheInitialPost, message);
    }
}
