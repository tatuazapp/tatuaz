using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Gateway.Handlers.Queries.Identity;
using Tatuaz.Gateway.Requests.Queries.Identity;

namespace Tatuaz.Gateway.Handlers;

public static class GatewayHandlersExtensions
{
    public static IServiceCollection RegisterGatewayHandlersServices(
        this IServiceCollection services
    )
    {
        services.AddMediatR(typeof(MeQuery).Assembly, typeof(MeQueryHandler).Assembly);
        return services;
    }
}
