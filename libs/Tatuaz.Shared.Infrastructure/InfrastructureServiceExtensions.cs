using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;

namespace Tatuaz.Shared.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public const string DefaultConnectionStringName = "TatuazMainDb";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<MainDbContext>(opt =>
        {
            opt.UseNpgsql(
                configuration.GetConnectionString(DefaultConnectionStringName),
                npgsqlOpt =>
                {
                    npgsqlOpt.EnableRetryOnFailure(5);
                    npgsqlOpt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }
            );
            opt.UseNpgsql(configuration.GetConnectionString(DefaultConnectionStringName));
            opt.UseSnakeCaseNamingConvention();
        });

        services.AddIdentityCore<TatuazUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 1;
                opt.SignIn.RequireConfirmedAccount = false;
                opt.SignIn.RequireConfirmedEmail = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddRoles<TatuazRole>()
            .AddEntityFrameworkStores<MainDbContext>();

        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IGenericRepository<,,,>), typeof(GenericRepository<,,,>));
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<IClock>(_ => SystemClock.Instance);

        services.AddMassTransit(x => { x.UsingInMemory(); });

        return services;
    }
}
