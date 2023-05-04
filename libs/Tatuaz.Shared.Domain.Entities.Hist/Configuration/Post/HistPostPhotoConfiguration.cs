using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Post;

public class HistPostPhotoConfiguration : IEntityTypeConfiguration<HistPostPhoto>
{
    public void Configure(EntityTypeBuilder<HistPostPhoto> builder)
    {
        builder.ToTable("H_post_photos", HistTatuazPostConstants.SchemaName);

        builder.HasKey(x => x.HistId);
    }
}
