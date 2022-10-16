using MassTransit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using NodaTime;
using NodaTime.Testing;

using Tatuaz.History.Queue.Consumers;
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
            opt.UseNpgsql(config.GetConnectionString("InfrastructureTest"), npgsqlOpt => { npgsqlOpt.UseNodaTime(); });
            opt.UseSnakeCaseNamingConvention();
        });
        services.AddScoped<DbContext, BooksDbContext>();
        services.AddScoped<IClock>(_ => new FakeClock(Instant.FromUtc(2021, 1, 1, 0, 0)));

        // var endpointMock = new Mock<ISendEndpoint>();
        // endpointMock
        //     .Setup(x => x.Send(It.IsAny<IEnumerable<object>>(), CancellationToken.None))
        //     .Returns(Task.CompletedTask);
        // var endpointSetupMock = new Mock<ISendEndpointProvider>();
        // endpointSetupMock
        //     .Setup(x => x.GetSendEndpoint(It.IsAny<Uri>()))
        //     .Returns(Task.FromResult(endpointMock.Object));
        // services.AddScoped<ISendEndpointProvider>(_ => endpointSetupMock.Object);
        //
        services.AddMassTransit(x => {
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingInMemory();
        });
    }

    public void Configure(IServiceProvider applicationServices)
    {
        var dbContext = applicationServices.GetRequiredService<DbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
