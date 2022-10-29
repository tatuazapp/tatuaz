using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Gateway.Infrastructure;

public class GatewayDbContext : DbContext
{
    public GatewayDbContext(DbContextOptions<GatewayDbContext> options) : base(options)
    {
    }

    public DbSet<TatuazUser> TatuazUsers { get; set; } = default!;
    public DbSet<TatuazRole> TatuazRoles { get; set; } = default!;
    public DbSet<TatuazUserRole> TatuazUserRoles { get; set; } = default!;
}
