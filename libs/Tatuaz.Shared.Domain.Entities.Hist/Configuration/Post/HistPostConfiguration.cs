using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Post;

public class HistPostConfiguration : IEntityTypeConfiguration<HistPost>
{
    public void Configure(EntityTypeBuilder<HistPost> builder)
    {
        builder.ToTable("H_posts", HistTatuazPostConstants.SchemaName);

        builder.Property(x => x.AuthorId).HasMaxLength(320);

        builder.Property(x => x.Description).HasMaxLength(4096);
    }
}
