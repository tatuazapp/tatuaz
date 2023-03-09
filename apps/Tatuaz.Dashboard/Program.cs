#pragma warning disable CA1852
using Microsoft.Extensions.Hosting;
using Tatuaz.Dashboard;
using Tatuaz.Shared.Infrastructure;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(
    (host, services) =>
    {
        services.RegisterDashboardServices(host.Configuration);
    }
);

builder.ConfigureAppConfiguration(config =>
{
    builder.RegisterDashboardHost(config.Build());
});
var app = builder.Build();

await app.MigrateDatabaseInDevelopmentAsync().ConfigureAwait(false);

app.Run();

namespace Tatuaz.Dashboard
{
    internal sealed partial class Program { }
}
