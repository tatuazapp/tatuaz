using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Photo;

public class HistPhotoConfiguration : IEntityTypeConfiguration<HistPhoto>
{
    public void Configure(EntityTypeBuilder<HistPhoto> builder)
    {
        builder.ToTable("H_photos", HistTatuazPhotoConstants.SchemaName);

        builder.HasKey(x => x.HistId);
    }
}
