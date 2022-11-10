using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Gateway.Queue.Producers;
using Tatuaz.Gateway.Queue.Producers.Landing.ListArtistStats;

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
