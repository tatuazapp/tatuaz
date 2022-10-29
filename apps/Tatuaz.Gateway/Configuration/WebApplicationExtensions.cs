using Microsoft.Extensions.Options;
using Tatuaz.Gateway.Configuration.Options;
using Tatuaz.Gateway.Middleware;

namespace Tatuaz.Gateway.Configuration;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Services.GetService<IOptions<SwaggerOptions>>()!.Value.Enabled)
        {
            app.UseSwagger(cfg => cfg.RouteTemplate = "api-docs/{documentName}/swagger.json");
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/api-docs/v1/swagger.json", "tatuaz.app API");
                cfg.RoutePrefix = "api-docs";
            });
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
