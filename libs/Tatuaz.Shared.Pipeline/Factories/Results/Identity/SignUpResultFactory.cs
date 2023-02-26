using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results.Identity;

public static class SignUpResultFactory
{
    public static TatuazResult<T> UserAlreadyExists<T>(
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest
    )
    {
        return new TatuazResult<T>(
            new[] { SignUpErrorFactory.UserAlreadyExistsError(message) },
            httpStatusCode
        );
    }
}
