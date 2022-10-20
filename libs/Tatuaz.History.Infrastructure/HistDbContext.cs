using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.DataAccess;

public class HistDbContext : DbContext
{
    public HistDbContext(DbContextOptions<HistDbContext> options) : base(options) { }

    public HistDbContext() { }

    public IQueryable<HistEntity> GetHistEntities(Type type)
    {
        switch (type)
        {
            default:
                throw new ArgumentException("Unknown type");
        }
    }
}
