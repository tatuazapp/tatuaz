using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Tatuaz.Gateway.Configuration.Helpers;
using Tatuaz.Gateway.Configuration.Options;

namespace Tatuaz.Gateway.Configuration;

public static class ServiceExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SwaggerOptions>(configuration.GetSection(SwaggerOptions.SectionName));
        services.Configure<Auth0Options>(configuration.GetSection(Auth0Options.SectionName));
        return services;
    }

    /// <summary>
    /// Add Gateway services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddGatewayServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTatuazControllers();
        services.AddTatuazSwagger();
        services.AddTatuazCors();
        services.AddTatuazAuth0(configuration);

        return services;
    }

    /// <summary>
    /// Add Tatuaz Controllers
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    public static IServiceCollection AddTatuazControllers(this IServiceCollection service)
    {
        service
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            });

        return service;
    }

    /// <summary>
    ///    Add Swagger to the project
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddTatuazSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "tatuaz.app API",
                    Description = "API for tatuaz.app"
                }
            );
            opt.IncludeXmlComments($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            opt.SupportNonNullableReferenceTypes();
            opt.CustomSchemaIds(type => type.ShortDisplayName().Replace('<', '_').Replace(">", ""));
        });

        return services;
    }

    /// <summary>
    ///  Cors policy name
    /// </summary>
    public static string TatuazCorsName => "AllowAll";

    /// <summary>
    /// Add cors policy
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddTatuazCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                TatuazCorsName,
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }
            );
        });

        return services;
    }

    /// <summary>
    ///    Add Auth0 authentication
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddTatuazAuth0(this IServiceCollection services,
        IConfiguration configuration)
    {
        var auth0Options = configuration.GetAuth0Options();

        var auth0Token = Auth0Helpers.GetAuth0Token(auth0Options);

        var apiIdentifier = $"https://{auth0Options.Domain}/api/v2";
        services.AddScoped<IManagementApiClient>(_ =>
            new ManagementApiClient(auth0Token, new Uri(apiIdentifier)));

        var domain = $"https://{auth0Options.Domain}/";
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = domain;
            options.Audience = apiIdentifier;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

        return services;
    }
}
