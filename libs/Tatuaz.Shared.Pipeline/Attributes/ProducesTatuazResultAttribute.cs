namespace Tatuaz.Shared.Pipeline.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ProducesTatuazResultAttribute : Attribute
{
    public Type ResultType { get; }

    public ProducesTatuazResultAttribute(Type resultType)
    {
        ResultType = resultType;
    }
}
