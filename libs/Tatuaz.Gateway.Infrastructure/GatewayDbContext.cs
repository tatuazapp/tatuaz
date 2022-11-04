using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Gateway.Infrastructure;

public class GatewayDbContext : DbContext
{
    public GatewayDbContext(DbContextOptions<GatewayDbContext> options) : base(options)
    {
    }

    public GatewayDbContext()
    {
    }

    public virtual DbSet<TatuazUser> TatuazUsers { get; set; } = default!;
    public virtual DbSet<TatuazRole> TatuazRoles { get; set; } = default!;
    public virtual DbSet<TatuazUserRole> TatuazUserRoles { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(Entity<,>).Assembly);
    }
}