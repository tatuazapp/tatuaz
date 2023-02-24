using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    )
        where TDbContext : DbContext
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.UseNodaTime();
        dataSourceBuilder.MapEnum<HistState>();
        dataSourceBuilder.MapEnum<PhotoCategoryType>();
        dataSourceBuilder.MapEnum<HistPhotoCategoryType>();
        var dataSource = dataSourceBuilder.Build();
        services.AddDbContext<TDbContext>(opt =>
        {
            opt.UseNpgsql(
                dataSource,
                npgsqlOpt =>
                {
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

    public static async Task MigrateDatabaseInDevelopmentAsync(this IHost host)
    {
        if (!host.Services.GetRequiredService<IHostEnvironment>().IsDevelopment())
        {
            return;
        }
        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
        await dbContext.Database.MigrateAsync().ConfigureAwait(false);
    }
}
