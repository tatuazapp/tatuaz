using Microsoft.EntityFrameworkCore;

namespace Tatuaz.History.DataAccess;

public class HistDbContext : DbContext
{
    public HistDbContext(DbContextOptions<HistDbContext> options) : base(options)
    {
    }

    public HistDbContext()
    {
    }
}
