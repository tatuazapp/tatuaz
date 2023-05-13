using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IO;
using Microsoft.OpenApi.Models;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using SixLabors.ImageSharp.Web.Caching.Azure;
using SixLabors.ImageSharp.Web.Commands;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Web.Processors;
using SixLabors.ImageSharp.Web.Providers.Azure;
using SixLabors.ImageSharp.Web.Resolvers.Azure;
using Tatuaz.Dashboard.Queue;
using Tatuaz.Gateway.Authorization;
using Tatuaz.Gateway.Configuration;
using Tatuaz.Gateway.Handlers;
using Tatuaz.Gateway.Swagger;
using Tatuaz.Scheduler.Queue;
using Tatuaz.Shared.Domain.Dtos;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Helpers;
using Tatuaz.Shared.Infrastructure;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Shared.Pipeline;
using Tatuaz.Shared.Pipeline.Configuration;
using Tatuaz.Shared.Pipeline.Filters;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.UserContext;
using Tatuaz.Shared.Services;

namespace Tatuaz.Gateway;

/// <summary>
/// Extensions for configuring gateway services and host
/// </summary>
public static class GatewayExtensions
{
    /// <summary>
    /// Cors policy name
    /// </summary>
    public static string TatuazCorsName => "AllowAll";

    /// <summary>
    /// Configure gateway host
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static ConfigureHostBuilder RegisterGatewayHost(
        this ConfigureHostBuilder host,
        IConfiguration configuration
    )
    {
        var serilogOpt = GetSerilogOpt(configuration);
        host.UseSerilog(
            (context, services, loggerConfiguration) =>
            {
                if (services.GetRequiredService<IHostEnvironment>().IsDevelopment())
                {
                    loggerConfiguration.WriteTo.Async(
                        x =>
                            x.Console(
                                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                                levelSwitch: new LoggingLevelSwitch(
                                    StringHelpers.GetLoggingLevelSwitch(serilogOpt.ConsoleLogLevel)
                                ),
                                formatProvider: new CultureInfo("en-US")
                            )
                    );

                    loggerConfiguration.WriteTo.Async(x =>
                    {
                        x.File(
                            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                            path: "logs/gateway.log",
                            rollingInterval: RollingInterval.Day,
                            levelSwitch: new LoggingLevelSwitch(
                                StringHelpers.GetLoggingLevelSwitch(serilogOpt.ConsoleLogLevel)
                            ),
                            formatProvider: new CultureInfo("en-US")
                        );
                    });
                }

                loggerConfiguration.WriteTo.AzureBlobStorage(
                    serilogOpt.BlobConnectionString,
                    StringHelpers.GetLoggingLevelSwitch(serilogOpt.CloudLogLevel),
                    storageContainerName: serilogOpt.BlobContainerName,
                    storageFileName: serilogOpt.BlobFileName,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                    formatProvider: new CultureInfo("en-US"),
                    writeInBatches: true,
                    period: TimeSpan.FromSeconds(30)
                );

                loggerConfiguration.Enrich.FromLogContext();
                loggerConfiguration.Enrich.FromMassTransit();
            }
        );

        return host;
    }

    /// <summary>
    /// Configure gateway services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IServiceCollection RegisterGatewayServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<SwaggerOpt>(configuration.GetSection(SwaggerOpt.SectionName));
        services.Configure<AuthOpt>(configuration.GetSection(AuthOpt.SectionName));
        services.Configure<RabbitMqOpt>(configuration.GetSection(RabbitMqOpt.SectionName));
        services.Configure<BlobOpt>(configuration.GetSection(BlobOpt.SectionName));
        services.Configure<SerilogOpt>(configuration.GetSection(SerilogOpt.SectionName));

        services
            .AddControllers(opt =>
            {
                opt.Filters.Add<UserContextActionFilter>();
            })
            .ConfigureApiBehaviorOptions(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(
                            x => new TatuazError("InvalidModel", x.ErrorMessage ?? "Invalid model")
                        )
                        .ToArray();

                    var errorResponse = new ObjectResult(HttpHelpers.ToErrorsObject(errors))
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };

                    return errorResponse;
                };
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
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

            opt.CustomOperationIds(x => x.RelativePath?.Split("/").Last());
            opt.SchemaFilter<FluentValidationSchemaFilter>();
            opt.SchemaFilter<MarkRequiredSchemaFilter>();
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

        services.AddValidatorsFromAssemblies(new[] { typeof(SignUpDto).Assembly });

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

        services.RegisterSharedInfrastructureServices<MainDbContext>(
            configuration.GetConnectionString(
                SharedInfrastructureExtensions.MainDbConnectionStringName
            ) ?? throw new Exception("Connection string not found")
        );

        services
            .AddImageSharp(opt =>
            {
                opt.OnParseCommandsAsync = c =>
                {
                    if (c.Commands.Count == 0)
                    {
                        return Task.CompletedTask;
                    }

                    var width = c.Parser.ParseValue<uint>(
                        c.Commands.GetValueOrDefault(ResizeWebProcessor.Width),
                        c.Culture
                    );

                    var height = c.Parser.ParseValue<uint>(
                        c.Commands.GetValueOrDefault(ResizeWebProcessor.Height),
                        c.Culture
                    );

                    if (width > 4000)
                        c.Commands.Remove(ResizeWebProcessor.Width);
                    if (height > 4000)
                        c.Commands.Remove(ResizeWebProcessor.Height);

                    return Task.CompletedTask;
                };
                opt.MemoryStreamManager = new RecyclableMemoryStreamManager(
                    1024,
                    1024 * 1024,
                    16 * 1024 * 1024
                );
            })
            .ClearProviders()
            .Configure<AzureBlobStorageImageProviderOptions>(opt =>
            {
                opt.BlobContainers.Add(
                    new AzureBlobContainerClientOptions()
                    {
                        ConnectionString = GetBlobOpt(configuration).ConnectionString,
                        ContainerName = GetBlobOpt(configuration).ImagesContainerName
                    }
                );
            })
            .Configure<AzureBlobStorageCacheOptions>(opt =>
            {
                opt.ConnectionString = GetBlobOpt(configuration).ConnectionString;
                opt.ContainerName = GetBlobOpt(configuration).ImagesCacheContainerName;

                AzureBlobStorageCache.CreateIfNotExists(opt, PublicAccessType.BlobContainer);
            })
            .AddProvider<AzureBlobStorageImageProvider>()
            .SetCache<AzureBlobStorageCache>()
            .RemoveProcessor<BackgroundColorWebProcessor>();

        services.RegisterGatewayHandlersServices();

        services.RegisterSharedDomainDtosServices();

        services.RegisterSharedPipelineServices(configuration);

        services.RegisterDashboardQueueServices();

        services.RegisterSchedulerQueueServices();

        services.RegisterSharedServicesServices();

        services.AddHttpContextAccessor();
        services.AddSingleton<IUserContext, UserContext>();

        return services;
    }

    /// <summary>
    /// Read auth0 options from configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static AuthOpt GetAuthOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(AuthOpt.SectionName).Get<AuthOpt>()
            ?? throw new Exception("Auth options not found");
    }

    /// <summary>
    /// Read swagger options from configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static SwaggerOpt GetSwaggerOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(SwaggerOpt.SectionName).Get<SwaggerOpt>()
            ?? throw new Exception("Swagger options not found");
    }

    public static SerilogOpt GetSerilogOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(SerilogOpt.SectionName).Get<SerilogOpt>()
            ?? throw new Exception("Serilog options not found");
    }

    public static BlobOpt GetBlobOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(BlobOpt.SectionName).Get<BlobOpt>()
            ?? throw new Exception("Blob options not found");
    }
}
