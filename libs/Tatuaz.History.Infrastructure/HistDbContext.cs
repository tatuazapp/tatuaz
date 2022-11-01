using Microsoft.EntityFrameworkCore;
using Npgsql;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.History.DataAccess;

public class HistDbContext : DbContext
{
    static HistDbContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<HistState>();
    public HistDbContext(DbContextOptions<HistDbContext> options) : base(options)
    {
    }

    public HistDbContext()
    {
    }

    public DbSet<HistTatuazUser> HTatuazUsers { get; set; } = default!;
    public DbSet<HistTatuazRole> HTatuazRoles { get; set; } = default!;
    public DbSet<HistTatuazUserRole> HTatuazUserRoles { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HistEntity<>).Assembly);
        builder.HasPostgresEnum<HistState>();
    }
}
