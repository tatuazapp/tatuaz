using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;
using NodaTime.Testing;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple;
using Tatuaz.Testing.Fakes.Common;
using Tatuaz.Testing.Fakes.Infrastructure;
using Tatuaz.Testing.Mocks.Queues;

namespace Tatuaz.Shared.Infrastructure.Test;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
    }

    public void ConfigureServices(
        IServiceCollection services,
        HostBuilderContext hostBuilderContext
    )
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.InfrastructureTest.json")
            .AddEnvironmentVariables("TATUAZ_")
            .Build();

        services.AddSingleton<IPrimitiveValuesGenerator, PrimitiveValuesGenerator>();
        services.AddScoped<IUserAccessor, UserAccessorFake>();
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IGenericRepository<,,,>), typeof(GenericRepository<,,,>));
        services.AddDbContext<BooksDbContext>(opt =>
        {
            opt.UseNpgsql(
                config.GetConnectionString("InfrastructureTest"),
                npgsqlOpt => { npgsqlOpt.UseNodaTime(); }
            );
            opt.UseSnakeCaseNamingConvention();
        });
        services.AddScoped<IClock>(_ => new FakeClock(Instant.FromUtc(2021, 1, 1, 0, 0)));

        services.RegisterQueuesMocks();
    }

    public void Configure(IServiceProvider applicationServices)
    {
        var dbContext = applicationServices.GetRequiredService<BooksDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
