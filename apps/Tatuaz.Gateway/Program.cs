using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Tatuaz.Gateway.Configuration;

var builder = WebApplication.CreateBuilder(args);

var logger = LoggerFactory.Create(config => { config.AddConsole(); }).CreateLogger("Program");

builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddGatewayServices(builder.Configuration);

logger.LogInformation("Added Gateway Services");

var app = builder.Build();

app.ConfigurePipeline();

logger.LogInformation("Configured Pipeline");

app.Run();
