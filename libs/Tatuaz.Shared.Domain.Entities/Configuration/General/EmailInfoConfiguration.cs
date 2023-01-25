using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Configuration.General;

public class EmailInfoConfiguration : IEntityTypeConfiguration<EmailInfo>
{
    public void Configure(EntityTypeBuilder<EmailInfo> builder)
    {
        builder.ToTable("email_info", TatuazGeneralConstants.SchemaName);

        builder.Property(x => x.RecipientEmail).HasMaxLength(320);

        builder.Property(x => x.EmailType).HasMaxLength(50);

        builder.Property(x => x.Error).HasMaxLength(500);

        builder.HasIndex(x => x.RecipientEmail);
    }
}
