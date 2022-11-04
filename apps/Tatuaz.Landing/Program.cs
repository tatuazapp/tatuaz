using Microsoft.AspNetCore.Builder;
using Tatuaz.Landing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterLandingServices(builder.Configuration);

builder.Host.RegisterLandingHost();

builder.Build().Run();
