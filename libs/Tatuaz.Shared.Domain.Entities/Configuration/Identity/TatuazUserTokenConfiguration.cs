using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazUserTokenConfiguration : IEntityTypeConfiguration<TatuazUserToken>
{
    public void Configure(EntityTypeBuilder<TatuazUserToken> builder)
    {
        builder.ToTable("tatuaz_user_tokens", TatuazIdentityConstants.SchemaName);

        builder.HasOne<TatuazUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_tatuaz_user_tokens_tatuaz_users_user_id");
    }
}
