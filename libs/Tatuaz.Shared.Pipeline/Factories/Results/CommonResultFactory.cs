using System.Net;
using Tatuaz.Shared.Pipeline.Factories.Errors;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Results;

public static class CommonResultFactory
{
    public static TatuazResult<T> Ok<T>(T Data, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        return new TatuazResult<T>(Data, Array.Empty<TatuazError>(), httpStatusCode);
    }

    public static TatuazResult<T> InternalError<T>(string? message = null, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
    {
        return new TatuazResult<T>(new[] { CommonErrorFactory.InternalError(message) }, httpStatusCode);
    }

    public static TatuazResult<T> DatabaseError<T>(string? message = null, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
    {
        return new TatuazResult<T>(new[] { CommonErrorFactory.DatabaseError(message) }, httpStatusCode);
    }

    public static TatuazResult<T> QueueError<T>(string? message = null, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
    {
        return new TatuazResult<T>(new[] { CommonErrorFactory.QueueError(message) }, httpStatusCode);
    }
}
