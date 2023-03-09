using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Post;

public class PostPhotoConfiguration : IEntityTypeConfiguration<PostPhoto>
{
    public void Configure(EntityTypeBuilder<PostPhoto> builder)
    {
        builder.ToTable("post_photos", TatuazPostConstants.SchemaName);
    }
}
