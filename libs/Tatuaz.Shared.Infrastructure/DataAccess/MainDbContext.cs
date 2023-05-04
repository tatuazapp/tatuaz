using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Models.Post;
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
    public virtual DbSet<Category> Categories { get; set; } = default!;
    public virtual DbSet<UserCategory> UserCategories { get; set; } = default!;
    public virtual DbSet<Photo> Photos { get; set; } = default!;
    public virtual DbSet<PhotoCategory> PhotoCategories { get; set; } = default!;

    // post
    public virtual DbSet<Comment> Comments { get; set; } = default!;
    public virtual DbSet<CommentLike> CommentLikes { get; set; } = default!;
    public virtual DbSet<InitialPost> InitialPosts { get; set; } = default!;
    public virtual DbSet<InitialPostPhoto> InitialPostPhotos { get; set; } = default!;
    public virtual DbSet<Post> Posts { get; set; } = default!;
    public virtual DbSet<PostLike> PostLikes { get; set; } = default!;
    public virtual DbSet<PostPhoto> PostPhotos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(Entity<,>).Assembly);
        builder.HasPostgresExtension("postgis");
    }
}
