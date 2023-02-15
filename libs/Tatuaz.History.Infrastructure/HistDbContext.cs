using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

namespace Tatuaz.History.DataAccess;

public class HistDbContext : DbContext
{
    public HistDbContext(DbContextOptions<HistDbContext> options)
        : base(options) { }

    public HistDbContext() { }

    // identity
    public DbSet<HistTatuazUser> HTatuazUsers { get; set; } = default!;
    public DbSet<HistTatuazRole> HTatuazRoles { get; set; } = default!;
    public DbSet<HistTatuazUserRole> HTatuazUserRoles { get; set; } = default!;

    // general
    public DbSet<HistEmailInfo> HEmailInfos { get; set; } = default!;

    // photos
    public DbSet<HistPhotoCategory> HPhotoCategories { get; set; } = default!;
    public virtual DbSet<HistUserPhotoCategory> HUserPhotoCategories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HistEntity<>).Assembly);
        builder.HasPostgresEnum<HistState>();
        builder.HasPostgresExtension("postgis");
    }
}
