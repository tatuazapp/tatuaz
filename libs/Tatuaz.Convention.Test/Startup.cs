using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tatuaz.Shared.Infrastructure;
using Tatuaz.Shared.Infrastructure.DataAccess;

namespace Tatuaz.Convention.Test;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder) { }

    public void ConfigureServices(
        IServiceCollection services,
        HostBuilderContext hostBuilderContext
    )
    {
        services.RegisterSharedInfrastructureServices<MainDbContext>("test");
    }

    public void Configure(IServiceProvider applicationServices) { }
}
