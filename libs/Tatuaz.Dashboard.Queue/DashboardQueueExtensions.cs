using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Dashboard.Queue.Producers.Identity;
using Tatuaz.Dashboard.Queue.Producers.Photo;

namespace Tatuaz.Dashboard.Queue;

public static class DashboardQueueExtensions
{
    public static IServiceCollection RegisterDashboardQueueServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<ListCategoriesProducer>();
        services.AddScoped<SetBackgroundPhotoProducer>();
        services.AddScoped<SetForegroundPhotoProducer>();
        services.AddScoped<DeleteBackgroundPhotoProducer>();
        services.AddScoped<DeleteForegroundPhotoProducer>();
        services.AddScoped<GetUserProducer>();

        return services;
    }
}
