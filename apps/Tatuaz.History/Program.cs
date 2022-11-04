using Microsoft.AspNetCore.Builder;
using Tatuaz.History;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterHistoryServices(builder.Configuration);

builder.Host.RegisterHistoryHost();

builder.Build().Run();