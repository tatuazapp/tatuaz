namespace Tatuaz.Shared.Pipeline.Messages;

public sealed record TatuazError
{
    internal TatuazError(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }
    public string Message { get; }
}