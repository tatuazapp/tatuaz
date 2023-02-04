#pragma warning disable CA1852
using Microsoft.AspNetCore.Builder;
using Tatuaz.Scheduler;

var builder = WebApplication.CreateBuilder();

builder.Configuration.RegisterSchedulerConfiguration();
builder.Services.RegisterSchedulerServices(builder.Configuration);
builder.Host.RegisterSchedulerHost();

var app = builder.Build();
app.Run();
