namespace Tatuaz.Shared.Infrastructure.Exceptions;

public class MultipleIncludeStrategiesException : Exception
{
    public MultipleIncludeStrategiesException() : base("Multiple include strategies can't be used in the same query.")
    {
    }
}
