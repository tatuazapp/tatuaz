using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Post;

public class HistInitialPostPhotoConfiguration : IEntityTypeConfiguration<HistInitialPostPhoto>
{
    public void Configure(EntityTypeBuilder<HistInitialPostPhoto> builder)
    {
        builder.ToTable("H_initial_post_photos", HistTatuazPostConstants.SchemaName);

        builder.HasKey(x => x.HistId);
    }
}
