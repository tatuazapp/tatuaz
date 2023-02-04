using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Npgsql;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Photo;
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
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.UseNodaTime();
        dataSourceBuilder.MapEnum<HistState>();
        dataSourceBuilder.MapEnum<PhotoCategoryType>();
        dataSourceBuilder.MapEnum<HistPhotoCategoryType>();
        services.AddDbContext<TDbContext>(opt =>
        {
            opt.UseNpgsql(
                dataSourceBuilder.Build(),
                npgsqlOpt =>
                {
                    npgsqlOpt.EnableRetryOnFailure(5);
                    npgsqlOpt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    npgsqlOpt.UseNodaTime();
                    npgsqlOpt.UseNetTopologySuite();
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
