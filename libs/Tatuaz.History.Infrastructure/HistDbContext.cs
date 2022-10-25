using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.History.DataAccess;

public class HistDbContext : DbContext
{
    public HistDbContext(DbContextOptions<HistDbContext> options) : base(options)
    {
    }

    public HistDbContext()
    {
    }

    public DbSet<HistTatuazUser> HTatuazUsers { get; set; }
    public DbSet<HistTatuazRole> HTatuazRoles { get; set; }
    public DbSet<HistTatuazUserRole> HTatuazUserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }
}
