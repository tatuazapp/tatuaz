using Tatuaz.Gateway.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddGatewayServices(builder.Configuration);

var app = builder.Build();
app.ConfigurePipeline();
app.Run();
