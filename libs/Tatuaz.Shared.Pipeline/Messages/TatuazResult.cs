using System;
using System.Linq;
using System.Net;

namespace Tatuaz.Shared.Pipeline.Messages;

public sealed record TatuazResult<T>(T? Value, TatuazError[] Errors, HttpStatusCode HttpStatusCode)
{
    internal TatuazResult(TatuazError[] errors, HttpStatusCode httpStatusCode) : this(default, errors, httpStatusCode)
    {
    }

    public T? Value { get; set; } = Value;
    public TatuazError[] Errors { get; set; } = Errors ?? throw new ArgumentNullException(nameof(Errors));
    public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode;
    public bool Successful => !Errors.Any();
}
