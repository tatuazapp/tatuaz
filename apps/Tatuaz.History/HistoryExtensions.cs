using System;
using System.Globalization;
using System.IO;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Tatuaz.History.DataAccess;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue;
using Tatuaz.History.Queue.Consumers.Common;
using Tatuaz.Shared.Helpers;
using Tatuaz.Shared.Infrastructure;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline;
using Tatuaz.Shared.Pipeline.Configuration;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.History;

public static class HistoryExtensions
{
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

    public static IHostBuilder RegisterHistoryHost(
        this IHostBuilder host,
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
                            path: "logs/history.log",
                            rollingInterval: RollingInterval.Day,
                            levelSwitch: new LoggingLevelSwitch(
                                StringHelpers.GetLoggingLevelSwitch(serilogOpt.FileLogLevel)
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

    public static SerilogOpt GetSerilogOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(SerilogOpt.SectionName).Get<SerilogOpt>()
            ?? throw new Exception("Serilog options not found");
    }
}
