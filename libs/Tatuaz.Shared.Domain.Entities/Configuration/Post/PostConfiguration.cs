using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Post;

public class PostConfiguration : IEntityTypeConfiguration<Models.Post.Post>
{
    public void Configure(EntityTypeBuilder<Models.Post.Post> builder)
    {
        builder.ToTable("posts", TatuazPostConstants.SchemaName);

        builder.Property(x => x.AuthorId).HasMaxLength(320);

        builder.Property(x => x.Description).HasMaxLength(4096);
    }
}
