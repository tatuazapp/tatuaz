using System.Linq.Expressions;

namespace Tatuaz.Shared.Infrastructure.Utils;

public static class ExpressionUtils
{
    public static Expression<Func<TToModel, TToProperty>> Cast<
        TFromModel,
        TToModel,
        TFromProperty,
        TToProperty
    >(Expression<Func<TFromModel, TFromProperty>> expression)
    {
        Expression converted = Expression.Convert(expression.Body, typeof(TToProperty));
        return Expression.Lambda<Func<TToModel, TToProperty>>(converted, expression.Parameters);
    }
}
