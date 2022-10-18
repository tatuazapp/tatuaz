using Microsoft.EntityFrameworkCore;

namespace Tatuaz.Shared.Infrastructure.DataAccess;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }
}