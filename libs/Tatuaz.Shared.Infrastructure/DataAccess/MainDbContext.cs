using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.General;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Infrastructure.DataAccess;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options) { }

    public MainDbContext() { }

    // identity
    public virtual DbSet<TatuazUser> TatuazUsers { get; set; } = default!;
    public virtual DbSet<TatuazRole> TatuazRoles { get; set; } = default!;
    public virtual DbSet<TatuazUserRole> TatuazUserRoles { get; set; } = default!;

    // general
    public virtual DbSet<EmailInfo> EmailInfos { get; set; } = default!;

    // photos
    public virtual DbSet<PhotoCategory> PhotoCategories { get; set; } = default!;
    public virtual DbSet<UserPhotoCategory> UserPhotoCategories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(Entity<,>).Assembly);
        builder.HasPostgresExtension("postgis");
    }
}
