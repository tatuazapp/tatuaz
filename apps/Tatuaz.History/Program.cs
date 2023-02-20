#pragma warning disable CA1852
using Microsoft.Extensions.Hosting;
using Tatuaz.History;
using Tatuaz.Shared.Infrastructure;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(
    (host, services) =>
    {
        services.RegisterHistoryServices(host.Configuration);
    }
);

builder.ConfigureAppConfiguration(config =>
{
    builder.RegisterHistoryHost(config.Build());
});

var app = builder.Build();

await app.MigrateDatabaseInDevelopmentAsync().ConfigureAwait(false);

app.Run();

namespace Tatuaz.History
{
    internal sealed partial class Program { }
}
