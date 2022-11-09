using Microsoft.AspNetCore.Builder;
using Tatuaz.History;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.RegisterConfiguration();

builder.Services.RegisterHistoryServices(builder.Configuration);

builder.Host.RegisterHistoryHost();

var app = builder.Build();

app.MapGet("/", () => "I'm alive");

app.Run();
