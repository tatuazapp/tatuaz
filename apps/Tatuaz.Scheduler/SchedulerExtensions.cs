using System;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Tatuaz.Dashboard.Queue;
using Tatuaz.Scheduler.Queue;
using Tatuaz.Scheduler.Queue.Consumers.Post;
using Tatuaz.Shared.Domain.Dtos;
using Tatuaz.Shared.Helpers;
using Tatuaz.Shared.Infrastructure;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Shared.Pipeline;
using Tatuaz.Shared.Pipeline.Configuration;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.TatuazSchedulerJobs;
using Tatuaz.TatuazSchedulerJobs.Post;

namespace Tatuaz.Scheduler;

public static class SchedulerExtensions
{
    public static IServiceCollection RegisterSchedulerServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.RegisterSharedInfrastructureServices<MainDbContext>(
            configuration.GetConnectionString(
                SharedInfrastructureExtensions.MainDbConnectionStringName
            ) ?? throw new Exception("Connection string not found")
        );

        services.RegisterSharedDomainDtosServices();
        services.RegisterDashboardQueueServices();

        services.RegisterSharedPipelineServices(configuration,
            new[] { typeof(SchedulePostIntegrityCheckConsumer).Assembly });

        services.AddQuartz(opt =>
        {
            opt.UseMicrosoftDependencyInjectionJobFactory();

            opt.AddJob<PostIntegrityCheckJob>(jobOpt =>
            {
                jobOpt.WithIdentity(PostIntegrityCheckJob.Key);
                jobOpt.StoreDurably();
            });

            opt.UseInMemoryStore();
        });

        services.AddQuartzHostedService(opt => { opt.AwaitApplicationStarted = true; });

        return services;
    }

    public static IHostBuilder RegisterSchedulerHost(
        this IHostBuilder host,
        IConfiguration configuration
    )
    {
        var serilogOpt = GetSerilogOpt(configuration);

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
                        path: "logs/Scheduler.log",
                        rollingInterval: RollingInterval.Day,
                        levelSwitch: new LoggingLevelSwitch(LogEventLevel.Debug),
                        formatProvider: new CultureInfo("en-US")
                    );
                });

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

                loggerConfiguration.WriteTo.Async(x =>
                {
                    x.File(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                        path: "logs/Scheduler_error.log",
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

    public static SerilogOpt GetSerilogOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(SerilogOpt.SectionName).Get<SerilogOpt>()
               ?? throw new Exception("Serilog options not found");
    }
}
