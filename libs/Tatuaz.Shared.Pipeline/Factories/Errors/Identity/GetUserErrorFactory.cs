using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Identity;

public static class GetUserErrorFactory
{
    public static TatuazError UserNotFound(string username)
    {
        return new TatuazError(
            GetUserErrorCodes.UserNotFound,
            $"User with username {username} not found"
        );
    }
}
