using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ProducesTatuazErrorAttribute : Attribute
{
    public TatuazError Error { get; }

    public ProducesTatuazErrorAttribute(TatuazError error)
    {
        Error = error;
    }
}
