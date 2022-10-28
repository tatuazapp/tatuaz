namespace Tatuaz.Shared.Pipeline.Messages;

public sealed record TatuazError
{
    public string Code { get; }
    public string Message { get; }

    internal TatuazError(string code, string message)
    {
        Code = code;
        Message = message;
    }
}
