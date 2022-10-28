using System.Net;

namespace Tatuaz.Shared.Pipeline.Messages;

public sealed record TatuazResult<T>
{
    public T? Value { get; }
    public TatuazError[] Errors { get; }
    public HttpStatusCode HttpStatusCode { get; }
    public bool Successful => !Errors.Any();

    internal TatuazResult(T? value, TatuazError[] errors, HttpStatusCode httpStatusCode)
    {
        Value = value;
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        HttpStatusCode = httpStatusCode;
    }

    internal TatuazResult(TatuazError[] errors, HttpStatusCode httpStatusCode) : this(default, errors, httpStatusCode)
    {
    }
}
