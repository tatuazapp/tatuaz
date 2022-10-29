namespace Tatuaz.Shared.Pipeline.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ProducesTatuazResultAttribute : Attribute
{
    public ProducesTatuazResultAttribute(Type resultType)
    {
        ResultType = resultType;
    }

    public Type ResultType { get; }
}
