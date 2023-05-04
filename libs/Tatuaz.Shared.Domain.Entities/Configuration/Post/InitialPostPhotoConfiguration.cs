using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Post;

public class InitialPostPhotoConfiguration : IEntityTypeConfiguration<InitialPostPhoto>
{
    public void Configure(EntityTypeBuilder<InitialPostPhoto> builder)
    {
        builder.ToTable("initial_post_photos", TatuazPostConstants.SchemaName);
    }
}
