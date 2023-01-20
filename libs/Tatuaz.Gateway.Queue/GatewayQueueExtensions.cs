using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Gateway.Queue.Producers.Landing.ListArtistStats;
using Tatuaz.Gateway.Queue.Producers.Landing.ListSummaryStats;

namespace Tatuaz.Gateway.Queue;

public static class GatewayQueueExtensions
{
    public static IServiceCollection RegisterGatewayQueueProducers(this IServiceCollection services)
    {
        services.AddScoped<ListArtistStatsProducer>();
        services.AddScoped<ListSummaryStatsProducer>();

        return services;
    }
}
