using System.Linq.Expressions;

using Tatuaz.Shared.Infrastructure.Utils;

namespace Tatuaz.Shared.Infrastructure.Specification;

public class IncludableFullSpecification<TEntity, TProperty> : FullSpecification<TEntity>
    where TEntity : class
{
    public IncludableFullSpecification<TEntity, TNextProperty> ThenInclude<TNextProperty>(
        Expression<Func<TProperty, TNextProperty>> navigationPropertyPath)
    {
        Includes.Add((ExpressionUtils.Cast<TProperty, object, TNextProperty, object>(navigationPropertyPath), false));
        return AsIncludeable<TNextProperty>();
    }
}
