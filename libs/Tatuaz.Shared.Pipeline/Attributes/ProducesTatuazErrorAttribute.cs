using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ProducesTatuazErrorAttribute : Attribute
{
    public ProducesTatuazErrorAttribute(TatuazError error)
    {
        Error = error;
    }

    public TatuazError Error { get; }
}
