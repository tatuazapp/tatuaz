using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors.Identity;

public class CreateUserErrorFactory
{
    public static TatuazError UserAlreadyExistsError(string? message = null)
    {
        return message is null
            ? new TatuazError(CreateUserErrorCodes.UserAlreadyExists, "User already exists")
            : new TatuazError(CreateUserErrorCodes.UserAlreadyExists, message);
    }
}
