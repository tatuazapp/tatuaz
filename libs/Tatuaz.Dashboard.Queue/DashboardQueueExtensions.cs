using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Dashboard.Queue.Producers.Photo;

namespace Tatuaz.Dashboard.Queue;

public static class DashboardQueueExtensions
{
    public static IServiceCollection RegisterDashboardQueueServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<ListPhotoCategoriesProducer>();

        return services;
    }
}
