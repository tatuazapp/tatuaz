using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Gateway.Handlers.Queries.Users;
using Tatuaz.Gateway.Requests.Queries.Users;

namespace Tatuaz.Gateway.Handlers;

public static class GatewayHandlersExtensions
{
    public static IServiceCollection RegisterGatewayHandlersServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(WhoAmIQuery).Assembly, typeof(WhoAmIQueryHandler).Assembly);
        return services;
    }
}