using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Infrastructure.DataAccess;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    public MainDbContext()
    {
    }

    // identity
    public DbSet<TatuazUser> TatuazUsers { get; set; } = default!;
    public DbSet<TatuazRole> TatuazRoles { get; set; } = default!;
    public DbSet<TatuazUserRole> TatuazUserRoles { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(Entity<,>).Assembly);
    }
}