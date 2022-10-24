using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Identity;

public class HistTatuazUserTokenConfiguration : IEntityTypeConfiguration<HistTatuazUserToken>
{
    public void Configure(EntityTypeBuilder<HistTatuazUserToken> builder)
    {
        builder.ToTable("H_tatuaz_user_tokens", HistTatuazIdentityConstants.SchemaName);

        builder.HasKey(x => x.HistId);
    }
}
