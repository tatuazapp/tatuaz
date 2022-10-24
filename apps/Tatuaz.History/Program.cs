using Microsoft.AspNetCore.Builder;
using Tatuaz.History.Configuration;
using Tatuaz.History.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddHistoryLogging();
builder.Services.AddHistoryQueue(builder.Configuration);
builder.Services.AddHistoryInfrastructure(builder.Configuration);

builder.Build().Run();
