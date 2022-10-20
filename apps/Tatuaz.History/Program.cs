using Microsoft.AspNetCore.Builder;
using Tatuaz.History.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddHistoryLogging();
builder.Services.AddHistoryQueue(builder.Configuration);
builder.Services.AddHistoryDatabaseProvider(builder.Configuration);

builder.Build().Run();
