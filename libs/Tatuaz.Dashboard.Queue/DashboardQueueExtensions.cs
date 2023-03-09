using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Dashboard.Queue.Contracts.Statistics;
using Tatuaz.Dashboard.Queue.Producers.Photo;

namespace Tatuaz.Dashboard.Queue;

public static class DashboardQueueExtensions
{
    public static IServiceCollection RegisterDashboardQueueServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<ListPhotoCategoriesProducer>();
        services.AddScoped<GetRegisteredStatsProducer>();

        return services;
    }
}
