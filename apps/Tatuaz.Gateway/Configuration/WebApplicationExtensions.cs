using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tatuaz.Gateway.Middleware;

namespace Tatuaz.Gateway.Configuration;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Services.GetService<IOptions<SwaggerOpt>>()!.Value.Enabled)
        {
            app.UseSwagger(cfg => cfg.RouteTemplate = "api-docs/{documentName}/swagger.json");
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/api-docs/v1/swagger.json", "tatuaz.app API");
                cfg.RoutePrefix = "api-docs";
                cfg.DocumentTitle = "Halloween API";
                cfg.InjectStylesheet("/Assets/swagger/halloween.css");
            });
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
