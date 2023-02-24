using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Photo;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories", TatuazPhotoConstants.SchemaName);

        builder.Property(x => x.Title).HasMaxLength(64);

        builder.Property(x => x.ImageUri).HasMaxLength(256);
    }
}
