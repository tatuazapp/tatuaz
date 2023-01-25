#pragma warning disable CA1852
using Microsoft.AspNetCore.Builder;
using Tatuaz.Landing;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.RegisterLandingConfiguration();

builder.Services.RegisterLandingServices(builder.Configuration);

builder.Host.RegisterLandingHost();

var app = builder.Build();

app.MapGet("/", () => "I'm alive");

app.Run();

namespace Tatuaz.Landing
{
    internal sealed partial class Program { }
}
