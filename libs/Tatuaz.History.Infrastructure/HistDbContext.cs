using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

namespace Tatuaz.History.DataAccess;

public class HistDbContext : DbContext
{
    public HistDbContext(DbContextOptions<HistDbContext> options)
        : base(options) { }

    public HistDbContext() { }

    // identity
    public virtual DbSet<HistTatuazUser> HTatuazUsers { get; set; } = default!;
    public virtual DbSet<HistTatuazRole> HTatuazRoles { get; set; } = default!;
    public virtual DbSet<HistTatuazUserRole> HTatuazUserRoles { get; set; } = default!;

    // general
    public virtual DbSet<HistEmailInfo> HEmailInfos { get; set; } = default!;

    // photos
    public virtual DbSet<HistCategory> HCategories { get; set; } = default!;
    public virtual DbSet<HistUserCategory> HUserCategories { get; set; } = default!;
    public virtual DbSet<HistPhoto> HPhotos { get; set; } = default!;
    public virtual DbSet<HistPhotoCategory> HPhotoCategories { get; set; } = default!;

    // post
    public virtual DbSet<HistComment> HComments { get; set; } = default!;
    public virtual DbSet<HistCommentLike> HCommentLikes { get; set; } = default!;
    public virtual DbSet<HistInitialPost> HInitialPosts { get; set; } = default!;
    public virtual DbSet<HistInitialPostPhoto> HInitialPostPhotos { get; set; } = default!;
    public virtual DbSet<HistPost> HPosts { get; set; } = default!;
    public virtual DbSet<HistPostLike> HPostLikes { get; set; } = default!;
    public virtual DbSet<HistPostPhoto> HPostPhotos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HistEntity<>).Assembly);
        builder.HasPostgresExtension("postgis");
    }
}
