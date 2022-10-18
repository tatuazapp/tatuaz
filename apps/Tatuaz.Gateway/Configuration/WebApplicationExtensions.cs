using Microsoft.Extensions.Options;
using Tatuaz.Gateway.Configuration.Options;

namespace Tatuaz.Gateway.Configuration;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Services.GetService<IOptions<SwaggerOptions>>()!.Value.Enabled)
        {
            app.UseSwagger(cfg => { cfg.RouteTemplate = "api/swagger/{documentname}/swagger.json"; });
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/api/swagger/v1/swagger.json", "tatuaz.app API");
                cfg.RoutePrefix = "api/swagger";
            });
        }

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}