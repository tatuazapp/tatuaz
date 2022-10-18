using Microsoft.Extensions.DependencyInjection;

namespace Tatuaz.Testing.Mocks.Queues;

public static class QueuesServicesExtensions
{
    public static IServiceCollection RegisterQueuesMocks(this IServiceCollection services)
    {
        services.AddScoped(_ => new SendEndpointProviderMock().Object);

        return services;
    }
}
