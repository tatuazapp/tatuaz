using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.Configuration;
using Tatuaz.Gateway.Handlers;
using Tatuaz.Gateway.Infrastructure;
using Tatuaz.Gateway.Queue;
using Tatuaz.Gateway.Swagger;
using Tatuaz.Shared.Domain.Dtos;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Infrastructure;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline;
using Tatuaz.Shared.Pipeline.Configuration.Options;

namespace Tatuaz.Gateway;

public static class GatewayExtensions
{
    public static string TatuazCorsName => "AllowAll";

    public static ConfigureHostBuilder RegisterGatewatHost(this ConfigureHostBuilder host)
    {
        host.UseSerilog(
            (context, services, loggerConfiguration) =>
            {
                loggerConfiguration.WriteTo.Async(
                    x =>
                        x.Console(
                            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                            levelSwitch: new LoggingLevelSwitch(LogEventLevel.Debug)
                        )
                );

                loggerConfiguration.WriteTo.Async(x =>
                {
                    x.File(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                        path: "logs/gateway.log",
                        rollingInterval: RollingInterval.Day,
                        levelSwitch: new LoggingLevelSwitch(LogEventLevel.Debug)
                    );
                });

                loggerConfiguration.WriteTo.Async(x =>
                {
                    x.File(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                        path: "logs/gateway_error.log",
                        rollingInterval: RollingInterval.Day,
                        restrictedToMinimumLevel: LogEventLevel.Error
                    );
                });

                loggerConfiguration.Enrich.FromLogContext();
                loggerConfiguration.Enrich.FromMassTransit();
            }
        );

        return host;
    }

    public static IServiceCollection RegisterGatewayServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<SwaggerOpt>(configuration.GetSection(SwaggerOpt.SectionName));
        services.Configure<AuthOpt>(configuration.GetSection(AuthOpt.SectionName));
        services.Configure<RabbitMqOpt>(configuration.GetSection(RabbitMqOpt.SectionName));

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                }
            );

            opt.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                }
            );
            opt.SchemaFilter<FluentValidationSchemaFilter>();
            opt.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "tatuaz.app API",
                    Description = "API for tatuaz.app"
                }
            );
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);
            opt.SupportNonNullableReferenceTypes();
            opt.CustomSchemaIds(type => type.ShortDisplayName().Replace('<', '_').Replace(">", ""));
        });

        services.AddValidatorsFromAssemblies(new[] { typeof(CreateUserDto).Assembly });

        services.AddCors(options =>
        {
            options.AddPolicy(
                TatuazCorsName,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                }
            );
        });

        var auth0Options = configuration.GetAuthOpt();

        var apiIdentifier = $"https://{auth0Options.Domain}/api/v2";
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
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
            opt.AddPolicy(
                ActiveUserRequirement.Name,
                policy => policy.AddRequirements(new ActiveUserRequirement())
            );
        });
        services.AddSingleton<IAuthorizationHandler, ActiveUserHandler>();

        services.RegisterSharedInfrastructureServices<GatewayDbContext>(
            configuration.GetConnectionString(
                SharedInfrastructureExtensions.MainDbConnectionStringName
            )
        );

        services.RegisterGatewayHandlersServices();

        services.RegisterSharedDomainDtosServices();

        services.RegisterSharedPipelineServices(configuration);

        services.AddHttpContextAccessor();
        services.AddSingleton<IUserAccessor, GatewayUserAccessor>();

        services.RegisterGatewayQueueProducers();

        return services;
    }

    public static AuthOpt GetAuthOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(AuthOpt.SectionName).Get<AuthOpt>();
    }

    public static SwaggerOpt GetSwaggerOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(SwaggerOpt.SectionName).Get<SwaggerOpt>();
    }
}
