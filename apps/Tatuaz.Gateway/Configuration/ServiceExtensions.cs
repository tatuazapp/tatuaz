using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.OpenApi.Models;
using Tatuaz.Gateway.Configuration.Options;

namespace Tatuaz.Gateway.Configuration;

public static class ServiceExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SwaggerOptions>(configuration.GetSection(SwaggerOptions.Swagger));
        return services;
    }

    public static IServiceCollection AddGatewayServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1",
                new OpenApiInfo { Version = "v1", Title = "tatuaz.app API", Description = "API for tatuaz.app" });
            opt.IncludeXmlComments($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            opt.SupportNonNullableReferenceTypes();
            opt.CustomSchemaIds(type => type.ShortDisplayName().Replace('<', '_').Replace(">", ""));
        });

        return services;
    }
}
