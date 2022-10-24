using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazUserLoginConfiguration : IEntityTypeConfiguration<TatuazUserLogin>
{
    public void Configure(EntityTypeBuilder<TatuazUserLogin> builder)
    {
        builder.ToTable("tatuaz_user_logins", TatuazIdentityConstants.SchemaName);

        builder.HasOne<TatuazUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_tatuaz_user_logins_tatuaz_users_user_id");
    }
}
