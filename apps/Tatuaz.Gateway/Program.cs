using Microsoft.AspNetCore.Builder;
using Serilog;
using Tatuaz.Gateway;
using Tatuaz.Gateway.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterGatewayServices(builder.Configuration);

builder.Host.RegisterGatewatHost();

var swaggerOpt = builder.Configuration.GetSwaggerOpt();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

if (swaggerOpt.Enabled)
{
    app.UseSwagger(cfg => cfg.RouteTemplate = swaggerOpt.Route + "/{documentName}/swagger.json");
    app.UseSwaggerUI(cfg =>
    {
        cfg.SwaggerEndpoint($"/{swaggerOpt.Route}/v1/swagger.json", swaggerOpt.Name);
        cfg.RoutePrefix = swaggerOpt.Route;
        cfg.DocumentTitle = swaggerOpt.Title;
        if (!string.IsNullOrEmpty(swaggerOpt.Theme))
        {
            cfg.InjectStylesheet($"/Assets/swagger/{swaggerOpt.Theme}.css");

        }
    });
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();