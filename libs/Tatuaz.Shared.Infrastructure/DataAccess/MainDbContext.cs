using Microsoft.EntityFrameworkCore;

namespace Tatuaz.Shared.Infrastructure;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }
}
