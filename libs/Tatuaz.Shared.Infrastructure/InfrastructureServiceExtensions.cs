using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
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

        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IGenericRepository<,,,>), typeof(GenericRepository<,,,>));
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<IClock>(_ => SystemClock.Instance);

        services.AddMassTransit(x => { x.UsingInMemory(); });

        return services;
    }
}
