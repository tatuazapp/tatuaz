using Microsoft.AspNetCore.Builder;
using Tatuaz.Dashboard;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.RegisterDashboardConfiguration();

builder.Services.RegisterDashboardServices(builder.Configuration);

builder.Host.RegisterDashboardHost();

var app = builder.Build();

app.MapGet("/", () => "I'm alive");

app.Run();