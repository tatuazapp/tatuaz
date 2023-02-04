#pragma warning disable CA1852
using Microsoft.AspNetCore.Builder;
using Tatuaz.History;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.RegisterHistoryConfiguration();

builder.Services.RegisterHistoryServices(builder.Configuration);

builder.Host.RegisterHistoryHost();

var app = builder.Build();

app.MapGet("/", () => "I'm alive");

app.Run();

namespace Tatuaz.History
{
    internal sealed partial class Program { }
}
