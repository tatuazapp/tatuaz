using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.General;

public class HistEmailInfoConfiguration : IEntityTypeConfiguration<HistEmailInfo>
{
    public void Configure(EntityTypeBuilder<HistEmailInfo> builder)
    {
        builder.ToTable("H_email_info", HistTatuazGeneralConstants.SchemaName);

        builder.HasKey(x => x.HistId);

        builder.Property(x => x.RecipientEmail).HasMaxLength(320);

        builder.Property(x => x.EmailType).HasMaxLength(50);

        builder.Property(x => x.Error).HasMaxLength(500);
    }
}
