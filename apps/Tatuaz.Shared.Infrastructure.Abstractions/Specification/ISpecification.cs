using System.Linq.Expressions;

namespace Tatuaz.Shared.Infrastructure.Abstractions;

public interface ISpecification<T>
{
    public Func<IQueryable<T>, IQueryable<T>>? ApplyTracking();
    public Func<IQueryable<T>, IQueryable<T>>? ApplyFiltering();
    public Func<IQueryable<T>, IQueryable<T>>? ApplyOrdering();
    public Func<IQueryable<T>, IQueryable<T>>? ApplyIncludes();
    public Func<IQueryable<T>, IQueryable<T>>? ApplyPaging();
}
