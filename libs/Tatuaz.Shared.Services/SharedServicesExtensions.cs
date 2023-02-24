using Microsoft.Extensions.DependencyInjection;

namespace Tatuaz.Shared.Services;

public static class SharedServicesExtensions
{
    public static IServiceCollection RegisterSharedServicesServices(this IServiceCollection services)
    {
        services.AddScoped<IPhotoService, PhotoService>();

        return services;
    }
}
