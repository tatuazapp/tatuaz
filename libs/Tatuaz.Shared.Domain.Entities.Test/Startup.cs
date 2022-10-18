using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;
using NodaTime.Testing;

namespace Tatuaz.Shared.Domain.Entities.Test;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
    }

    public void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
    {
        services.AddScoped<IClock>(_ => new FakeClock(Instant.FromUtc(2021, 1, 1, 0, 0)));
    }

    public void Configure(IServiceProvider applicationServices)
    {
    }
}
