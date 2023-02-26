using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Photo;

public class HistUserCategoryConfiguration : IEntityTypeConfiguration<HistUserCategory>
{
    public void Configure(EntityTypeBuilder<HistUserCategory> builder)
    {
        builder.ToTable("H_user_categories", HistTatuazPhotoConstants.SchemaName);

        builder.HasKey(x => x.HistId);

        builder.Property(x => x.UserId).HasMaxLength(320);
    }
}
