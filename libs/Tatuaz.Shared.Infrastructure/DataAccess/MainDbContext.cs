using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Infrastructure.DataAccess;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    // identity
    public DbSet<TatuazUser> TatuazUsers { get; set; }
    public DbSet<TatuazRole> TatuazRoles { get; set; }
    public DbSet<TatuazUserRole> TatuazUserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(Entity<,>).Assembly);
    }
}
