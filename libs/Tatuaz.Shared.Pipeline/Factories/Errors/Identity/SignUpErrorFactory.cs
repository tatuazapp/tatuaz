using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Identity;

public static class SignUpErrorFactory
{
    public static TatuazError UserAlreadyExistsError(string? message = null)
    {
        return message is null
            ? new TatuazError(SignUpErrorCodes.UserAlreadyExists, "User already exists")
            : new TatuazError(SignUpErrorCodes.UserAlreadyExists, message);
    }
}
