using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Testing.Mocks.Infrastructure;

namespace Tatuaz.Gateway.Handlers.Test;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CreateUserDto).Assembly);
        services.AddScoped(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
        services.AddScoped<DbContext>(_ => new GatewayDbContextMock().Object);
    }
}
