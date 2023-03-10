using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Post;

public class HistCommentLikeConfiguration : IEntityTypeConfiguration<HistCommentLike>
{
    public void Configure(EntityTypeBuilder<HistCommentLike> builder)
    {
        builder.ToTable("H_comment_likes", HistTatuazPostConstants.SchemaName);
    }
}
