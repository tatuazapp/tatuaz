using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Identity;

public class HistTatuazUserLoginConfiguration : IEntityTypeConfiguration<HistTatuazUserLogin>
{
    public void Configure(EntityTypeBuilder<HistTatuazUserLogin> builder)
    {
        builder.ToTable("H_tatuaz_user_logins", HistTatuazIdentityConstants.SchemaName);

        builder.HasKey(x => x.HistId);
    }
}
