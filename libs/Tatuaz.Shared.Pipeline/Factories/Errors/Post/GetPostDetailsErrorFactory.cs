using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Post;

public static class GetPostDetailsErrorFactory
{
    public static TatuazError PostDoesNotExist(string? message = null)
    {
        return message is null
            ? new TatuazError(GetPostDetailsErrorCodes.PostDoesNotExist, "Post does not exist")
            : new TatuazError(GetPostDetailsErrorCodes.PostDoesNotExist, message);
    }
}
