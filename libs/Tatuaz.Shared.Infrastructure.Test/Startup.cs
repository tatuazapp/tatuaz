using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Tatuaz.Shared.Infrastructure.Abstractions;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple;
using Tatuaz.Testing.Fakes.Common;
using Tatuaz.Testing.Fakes.Infrastructure;

namespace Tatuaz.Shared.Infrastructure.Test;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
    }

    public void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.InfrastructureTest.json")
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<IPrimitiveValuesGenerator, PrimitiveValuesGenerator>();
        services.AddScoped<IUserAccessor, UserAccessorFake>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
        services.AddDbContext<BooksDbContext>(opt => {
            opt.UseNpgsql(config.GetConnectionString("InfrastructureTest"));
        });
        services.AddScoped<DbContext, BooksDbContext>();
    }

    public void Configure(IServiceProvider applicationServices)
    {
        var dbContext = applicationServices.GetRequiredService<DbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
