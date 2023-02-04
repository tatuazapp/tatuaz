using System;
using System.Globalization;
using System.IO;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Tatuaz.History.DataAccess;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue;
using Tatuaz.History.Queue.Consumers.Common;
using Tatuaz.Shared.Infrastructure;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline;

namespace Tatuaz.History;

public static class HistoryExtensions
{
    public static IConfiguration RegisterHistoryConfiguration(this IConfiguration configuration)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                true
            )
            .AddEnvironmentVariables();

        return builder.Build();
    }

    public static IServiceCollection RegisterHistoryServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.RegisterSharedPipelineServices(
            configuration,
            new[] { typeof(DumpHistoryConsumer).Assembly }
        );

        services.AddScoped(typeof(IDumpHistoryService<,>), typeof(DumpHistoryService<,>));
        services.AddScoped(typeof(IHistorySearcherService<,>), typeof(HistorySearcherService<,>));
        services.AddSingleton<IUserContext, HistUserContext>();

        services.RegisterSharedInfrastructureServices<HistDbContext>(
            configuration.GetConnectionString(
                SharedInfrastructureExtensions.HistDbConnectionStringName
            ) ?? throw new Exception("Connection string not found")
        );
        return services;
    }

    public static ConfigureHostBuilder RegisterHistoryHost(this ConfigureHostBuilder host)
    {
        host.UseSerilog(
            (context, services, loggerConfiguration) =>
            {
                loggerConfiguration.WriteTo.Async(
                    x =>
                        x.Console(
                            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                            levelSwitch: new LoggingLevelSwitch(LogEventLevel.Debug),
                            formatProvider: new CultureInfo("en-US")
                        )
                );

                loggerConfiguration.WriteTo.Async(x =>
                {
                    x.File(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                        path: "logs/history.log",
                        rollingInterval: RollingInterval.Day,
                        levelSwitch: new LoggingLevelSwitch(LogEventLevel.Debug),
                        formatProvider: new CultureInfo("en-US")
                    );
                });

                loggerConfiguration.WriteTo.Async(x =>
                {
                    x.File(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                        path: "logs/history_error.log",
                        rollingInterval: RollingInterval.Day,
                        restrictedToMinimumLevel: LogEventLevel.Error,
                        formatProvider: new CultureInfo("en-US")
                    );
                });

                loggerConfiguration.Enrich.FromLogContext();
                loggerConfiguration.Enrich.FromMassTransit();
            }
        );

        return host;
    }
}
