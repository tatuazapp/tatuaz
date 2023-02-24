#pragma warning disable CA1852
using Microsoft.Extensions.Hosting;
using Tatuaz.Scheduler;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(
    (host, services) =>
    {
        services.RegisterSchedulerServices(host.Configuration);
    }
);

builder.ConfigureAppConfiguration(config =>
{
    builder.RegisterSchedulerHost(config.Build());
});

builder.Build().Run();
