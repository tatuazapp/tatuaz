using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;

namespace Tatuaz.Shared.Infrastructure;

public static class SharedInfrastructureExtensions
{
    public const string MainDbConnectionStringName = "TatuazMainDb";
    public const string HistDbConnectionStringName = "TatuazHistDb";

    public static IServiceCollection RegisterSharedInfrastructureServices<TDbContext>(
        this IServiceCollection services,
        string connectionString
    ) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(opt =>
        {
            opt.UseNpgsql(
                connectionString,
                npgsqlOpt =>
                {
                    npgsqlOpt.EnableRetryOnFailure(5);
                    npgsqlOpt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    npgsqlOpt.UseNodaTime();
                }
            );
            opt.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<DbContext, TDbContext>();
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
        services.AddScoped<IClock>(_ => SystemClock.Instance);

        return services;
    }
}
