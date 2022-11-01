using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

namespace Tatuaz.Gateway.Handlers.Test;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CreateUserDto).Assembly);
    }
}
