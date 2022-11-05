using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Tatuaz.Shared.Infrastructure.Abstractions.Specification;

namespace Tatuaz.Shared.Infrastructure.Specification;

public class FullSpecification<TEntity> : ISpecification<TEntity> where TEntity : class
{
    private readonly List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> _customs;
    private readonly List<Expression<Func<TEntity, bool>>> _filteringPredicates;

    private readonly List<
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>
    > _includes;

    private readonly List<Expression<Func<TEntity, object>>> _orderingPredicates;

    public FullSpecification()
    {
        TrackingStrategy = TrackingStrategy.NoTracking;
        OrderDirection = OrderDirection.Ascending;
        _filteringPredicates = new List<Expression<Func<TEntity, bool>>>();
        _orderingPredicates = new List<Expression<Func<TEntity, object>>>();
        _includes = new List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>();
        _customs = new List<Func<IQueryable<TEntity>, IQueryable<TEntity>>>();
    }

    public TrackingStrategy TrackingStrategy { get; set; }
    public OrderDirection OrderDirection { get; set; }

    public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
    {
        query = ApplyTracking(query);
        query = ApplyFiltering(query);
        query = ApplyOrdering(query);
        query = ApplyIncludes(query);
        query = ApplyCustoms(query);
        return query;
    }

    public FullSpecification<TEntity> AddFilter(Expression<Func<TEntity, bool>> predicate)
    {
        _filteringPredicates.Add(predicate);
        return this;
    }

    public FullSpecification<TEntity> AddOrder(Expression<Func<TEntity, object>> predicate)
    {
        _orderingPredicates.Add(predicate);
        return this;
    }

    public FullSpecification<TEntity> UseInclude(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> predicate
    )
    {
        _includes.Add(predicate);
        return this;
    }

    public FullSpecification<TEntity> UseCustom(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> predicate
    )
    {
        _customs.Add(predicate);
        return this;
    }

    private IQueryable<TEntity> ApplyTracking(IQueryable<TEntity> query)
    {
        if (TrackingStrategy == TrackingStrategy.NoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    private IQueryable<TEntity> ApplyFiltering(IQueryable<TEntity> query)
    {
        if (_filteringPredicates.Any())
        {
            query = _filteringPredicates.Aggregate(
                query,
                (current, predicate) => current.Where(predicate)
            );
        }

        return query;
    }

    private IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query)
    {
        if (_orderingPredicates.Any())
        {
            query =
                OrderDirection == OrderDirection.Ascending
                    ? query.OrderBy(_orderingPredicates[0])
                    : query.OrderByDescending(_orderingPredicates[0]);
            for (var i = 1; i < _orderingPredicates.Count; i++)
            {
                query =
                    OrderDirection == OrderDirection.Ascending
                        ? ((IOrderedQueryable<TEntity>)query).ThenBy(_orderingPredicates[i])
                        : ((IOrderedQueryable<TEntity>)query).ThenByDescending(
                            _orderingPredicates[i]
                        );
            }
        }

        return query;
    }

    private IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query)
    {
        if (_includes.Any())
        {
            query = _includes.Aggregate(query, (_, include) => include(query));
        }

        return query;
    }

    private IQueryable<TEntity> ApplyCustoms(IQueryable<TEntity> query)
    {
        if (_customs.Any())
        {
            query = _customs.Aggregate(query, (_, custom) => custom(query));
        }

        return query;
    }
}
