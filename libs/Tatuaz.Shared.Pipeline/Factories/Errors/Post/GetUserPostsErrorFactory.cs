using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Post;

public static class GetUserPostsErrorFactory
{
    public static TatuazError UserNotFound()
    {
        return new TatuazError(GetUserPostsErrorCodes.UserNotFound, "User not found");
    }
}
