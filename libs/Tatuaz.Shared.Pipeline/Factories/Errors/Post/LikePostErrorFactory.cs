using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Post;

public static class LikePostErrorFactory
{
    public static TatuazError PostNotFound(string? message = null)
    {
        return message is null
            ? new TatuazError(LikePostErrorCodes.PostNotFound, "Post not found")
            : new TatuazError(LikePostErrorCodes.PostNotFound, message);
    }

    public static TatuazError PostAlreadyLiked()
    {
        return new TatuazError(LikePostErrorCodes.PostAlreadyLiked, "Post already liked");
    }

    public static TatuazError PostAlreadyUnliked()
    {
        return new TatuazError(LikePostErrorCodes.PostAlreadyUnliked, "Post already unliked");
    }
}
