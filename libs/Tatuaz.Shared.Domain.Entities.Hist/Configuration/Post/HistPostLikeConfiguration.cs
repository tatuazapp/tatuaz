using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Post;

public class HistPostLikeConfiguration : IEntityTypeConfiguration<HistPostLike>
{
    public void Configure(EntityTypeBuilder<HistPostLike> builder)
    {
        builder.ToTable("H_post_likes", HistTatuazPostConstants.SchemaName);

        builder.Property(x => x.UserId).HasMaxLength(320);
    }
}
