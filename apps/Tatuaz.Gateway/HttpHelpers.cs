using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway;

public static class HttpHelpers
{
    public static object ToErrorsObject(params TatuazError[] errors)
    {
        return new { Errors = errors };
    }
}
