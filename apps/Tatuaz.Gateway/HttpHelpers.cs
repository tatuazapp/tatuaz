using Tatuaz.Gateway.HttpResponses;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway;

public static class HttpHelpers
{
    /// <summary>
    ///     Helper method for generating response.
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="TData"></typeparam>
    /// <returns></returns>
    public static OkResponse<TData> ToOkObject<TData>(TatuazResult<TData> result)
    {
        return new OkResponse<TData>(result.Value);
    }

    /// <summary>
    ///     Helper method for generating response.
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static ErrorResponse ToErrorsObject(params TatuazError[] errors)
    {
        return new ErrorResponse(errors);
    }
}