using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Infrastructure.DataAccess;

public class MainDbContext : IdentityDbContext<TatuazUser, TatuazRole, Guid, TatuazUserClaim, TatuazUserRole,
    TatuazUserLogin, TatuazRoleClaim, TatuazUserToken>
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(Entity<,>).Assembly);
    }
}
