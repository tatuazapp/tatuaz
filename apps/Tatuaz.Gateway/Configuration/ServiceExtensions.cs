using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NodaTime;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.Configuration.Helpers;
using Tatuaz.Gateway.Configuration.Options;
using Tatuaz.Gateway.Handlers.Queries.Users;
using Tatuaz.Gateway.Infrastructure;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;

namespace Tatuaz.Gateway.Configuration;

public static class ServiceExtensions
{
    public const string DefaultConnectionStringName = "TatuazMainDb";

    /// <summary>
    ///     Cors policy name
    /// </summary>
    public static string TatuazCorsName => "AllowAll";

    /// <summary>
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
    ///     Add Gateway services
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
        services.AddTatuazGatewayInfrastructure(configuration);
        services.AddGatewayMediator(configuration);
        services.AddGatewayMapper(configuration);
        services.AddGatewayMassTransit(configuration);

        return services;
    }

    /// <summary>
    ///     Add Tatuaz Controllers
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
    ///     Add Swagger to the project
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddTatuazSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(
                "v1",
                new OpenApiInfo { Version = "v1", Title = "tatuaz.app API", Description = "API for tatuaz.app" }
            );
            opt.IncludeXmlComments($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            opt.SupportNonNullableReferenceTypes();
            opt.CustomSchemaIds(type => type.ShortDisplayName().Replace('<', '_').Replace(">", ""));
        });

        return services;
    }

    /// <summary>
    ///     Add cors policy
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
    ///     Add Auth0 authentication
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddTatuazAuth0(this IServiceCollection services,
        IConfiguration configuration)
    {
        var auth0Options = configuration.GetAuth0Options();

        var apiIdentifier = $"https://{auth0Options.Domain}/api/v2";
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = auth0Options.Authority;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier,
                ValidAudiences = new[] { apiIdentifier, auth0Options.Audience }
            };
        });

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(ActiveUserRequirement.Name,
                policy => policy.AddRequirements(new ActiveUserRequirement()));
        });
        services.AddSingleton<IAuthorizationHandler, ActiveUserHandler>();

        return services;
    }

    public static IServiceCollection AddTatuazGatewayInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<GatewayDbContext>(opt =>
        {
            opt.UseNpgsql(
                configuration.GetConnectionString(DefaultConnectionStringName),
                npgsqlOpt =>
                {
                    npgsqlOpt.EnableRetryOnFailure(5);
                    npgsqlOpt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    npgsqlOpt.UseNodaTime();
                }
            );
            opt.UseNpgsql(configuration.GetConnectionString(DefaultConnectionStringName));
            opt.UseSnakeCaseNamingConvention();
        });

        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IGenericRepository<,,,>), typeof(GenericRepository<,,,>));

        services.AddSingleton<IUserAccessor, GatewayUserAccessor>();
        services.AddScoped<IClock>(_ => SystemClock.Instance);
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddGatewayMediator(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(typeof(WhoAmIQuery).Assembly, typeof(WhoAmIQueryHandler).Assembly);
        return services;
    }

    public static IServiceCollection AddGatewayMapper(this IServiceCollection services,
        IConfiguration configuration)
    {
        // TODO add hist assembly when available
        services.AddAutoMapper(typeof(UserDto).Assembly);
        return services;
    }

    public static IServiceCollection AddGatewayMassTransit(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq(
                (context, cfg) =>
                {
                    cfg.Host(
                        "localhost",
                        "/",
                        h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        }
                    );
                }
            );
        });
        return services;
    }
}
