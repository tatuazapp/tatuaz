using Microsoft.AspNetCore.Builder;
using Tatuaz.Dashboard;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.RegisterConfiguration();

builder.Services.RegisterLandingServices(builder.Configuration);

builder.Host.RegisterLandingHost();

var app = builder.Build();

app.MapGet("/", () => "I'm alive");

app.Run();
