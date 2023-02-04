using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;

namespace Tatuaz.Shared.Domain.Dtos;

public static class SharedDomainDtosExtensions
{
    public static IServiceCollection RegisterSharedDomainDtosServices(
        this IServiceCollection services
    )
    {
        // TODO Add Dtos.Hist later
        services.AddAutoMapper(typeof(UserDto).Assembly);

        return services;
    }
}
