using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Post;

public class HistCommentConfiguration : IEntityTypeConfiguration<HistComment>
{
    public void Configure(EntityTypeBuilder<HistComment> builder)
    {
        builder.ToTable("H_comments", HistTatuazPostConstants.SchemaName);

        builder.HasKey(x => x.HistId);

        builder.Property(x => x.Content).HasMaxLength(1024);
    }
}
