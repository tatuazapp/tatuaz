namespace Tatuaz.History.DataAccess.Exceptions;

public class HistException : Exception
{
    public HistException(string message) : base(message)
    {
    }

    public HistException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
