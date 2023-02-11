using System.Linq;

namespace Tatuaz.Shared.Infrastructure.Abstractions.Specification;

public interface ISpecification<TEntity> where TEntity : class
{
    public IQueryable<TEntity> Apply(IQueryable<TEntity> query);
}
