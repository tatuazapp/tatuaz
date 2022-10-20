using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tatuaz.History.DataAccess.Services;

namespace Tatuaz.History.Test;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder) { }

    public void ConfigureServices(
        IServiceCollection services,
        HostBuilderContext hostBuilderContext
    )
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq(
                (context, cfg) =>
                {
                    cfg.Host(
                        "localhost",
                        "/",
                        h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        }
                    );
                }
            );
        });

        services.AddScoped(typeof(IDumpHistoryService<,>), typeof(DumpHistoryService<,>));
    }

    public void Configure(IServiceProvider applicationServices) { }
}
