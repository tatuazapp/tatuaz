using Microsoft.Extensions.DependencyInjection;

namespace Tatuaz.Scheduler.Queue;

public static class SchedulerQueueExtensions
{
    public static IServiceCollection RegisterSchedulerQueueServices(
        this IServiceCollection services
    )
    {
        return services;
    }
}
