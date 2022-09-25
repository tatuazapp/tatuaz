using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Tatuaz.Shared.Infrastructure.Abstractions.Specification;
using Tatuaz.Shared.Infrastructure.Exceptions;
using Tatuaz.Shared.Infrastructure.Utils;

using Z.EntityFramework.Plus;

namespace Tatuaz.Shared.Infrastructure.Specification;

public class FullSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class
{
    protected bool BlockInefficientQueries;
    protected Expression<Func<TEntity, bool>> FilteringPredicate;
    protected List<(Expression<Func<object, object>> navigationPropertyPath, bool parent)> Includes;
    protected Expression<Func<TEntity, object>>[] OrderingPredicates;
    protected bool ReverseOrdering;
    protected bool Tracking;

    public FullSpecification()
    {
        Tracking = false;
        FilteringPredicate = x => true;
        OrderingPredicates = Array.Empty<Expression<Func<TEntity, object>>>();
        ReverseOrdering = false;
        Includes = new List<(Expression<Func<object, object>> navigationPropertyPath, bool parent)>();
        BlockInefficientQueries = true;
    }

    public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
    {
        query = ApplyTracking(query);
        query = ApplyFiltering(query);
        query = ApplyOrdering(query);
        query = ApplyIncludes(query);
        return query;
    }

    public FullSpecification<TEntity> UseTracking(bool value = true)
    {
        Tracking = value;
        return this;
    }

    public FullSpecification<TEntity> UseFiltering(Expression<Func<TEntity, bool>> predicate)
    {
        FilteringPredicate = predicate;
        return this;
    }

    public FullSpecification<TEntity> UseOrdering(params Expression<Func<TEntity, object>>[] predicates)
    {
        OrderingPredicates = predicates;
        ReverseOrdering = false;
        return this;
    }

    public FullSpecification<TEntity> UseReverseOrdering(params Expression<Func<TEntity, object>>[] predicates)
    {
        OrderingPredicates = predicates;
        ReverseOrdering = true;
        return this;
    }

    public IncludableFullSpecification<TEntity, TProperty> Include<TProperty>(
        Expression<Func<TEntity, TProperty>> navigationPropertyPath)
    {
        Includes.Add((ExpressionUtils.Cast<TEntity, object, TProperty, object>(navigationPropertyPath), false));
        return AsIncludeable<TProperty>();
    }

    public FullSpecification<TEntity> AllowInefficientQueries()
    {
        BlockInefficientQueries = false;
        return this;
    }

    protected IncludableFullSpecification<TEntity, TProperty> AsIncludeable<TProperty>()
    {
        var includable = new IncludableFullSpecification<TEntity, TProperty>();
        includable.Tracking = Tracking;
        includable.FilteringPredicate = FilteringPredicate;
        includable.OrderingPredicates = OrderingPredicates;
        includable.ReverseOrdering = ReverseOrdering;
        includable.Includes = Includes;
        return includable;
    }

    private IQueryable<TEntity> ApplyTracking(IQueryable<TEntity> query)
    {
        if (!Tracking) query = query.AsNoTracking();

        return query;
    }

    private IQueryable<TEntity> ApplyFiltering(IQueryable<TEntity> query)
    {
        query = query.Where(FilteringPredicate);

        return query;
    }

    private IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query)
    {
        if (OrderingPredicates.Length > 0)
            query = ReverseOrdering
                ? query.OrderByDescending(OrderingPredicates[0])
                : query.OrderBy(OrderingPredicates[0]);

        for (var i = 1; i < OrderingPredicates.Length; i++)
            query = ReverseOrdering
                ? ((IOrderedQueryable<TEntity>)query).ThenByDescending(OrderingPredicates[i])
                : ((IOrderedQueryable<TEntity>)query).ThenBy(OrderingPredicates[i]);

        return query;
    }

    private IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query)
    {
        var canPerformOptimizedInclude = Includes.All(x => x.parent);
        if (Tracking && canPerformOptimizedInclude)
            for (var i = 0; i < Includes.Count; i++)
                query.IncludeOptimized(Includes[i].navigationPropertyPath);
        else if (canPerformOptimizedInclude && BlockInefficientQueries)
            throw new InefficientQueryException(
                "Query can use IncludeOptimized considering " +
                "requested includes, but requires tracking for it. " +
                "If you are sure about using inefficient query call " +
                "AllowInefficientQueries on this specification.");
        else
            for (var i = 0; i < Includes.Count; i++)
                if (Includes[i].parent)
                    query.Include(Includes[i].navigationPropertyPath);
                else ((dynamic)query).ThenInclude(Includes[i].navigationPropertyPath);

        return query;
    }
}
