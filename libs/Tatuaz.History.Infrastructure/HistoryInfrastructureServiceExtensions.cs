using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.History.DataAccess.Services;

namespace Tatuaz.History.DataAccess;

public static class HistoryInfrastructureServiceExtensions
{
    public static IServiceCollection AddHistoryInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped(typeof(IDumpHistoryService<,>), typeof(DumpHistoryService<,>));
        services.AddScoped(typeof(IHistorySearcherService<,>), typeof(HistorySearcherService<,>));
        services.AddDbContext<HistDbContext>(opt =>
        {
            opt.UseNpgsql(
                configuration.GetConnectionString("TatuazHistory"),
                npgsqlOpt =>
                {
                    npgsqlOpt.EnableRetryOnFailure(5);
                    npgsqlOpt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    npgsqlOpt.UseNodaTime();
                }
            );
            opt.UseSnakeCaseNamingConvention();
        });
        return services;
    }
}
