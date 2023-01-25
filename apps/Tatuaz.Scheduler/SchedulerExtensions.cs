using System;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Tatuaz.Shared.Infrastructure;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Shared.Pipeline;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.TatuazSchedulerJobs;

namespace Tatuaz.Scheduler;

public static class SchedulerExtensions
{
    public static IConfiguration RegisterSchedulerConfiguration(this IConfiguration configuration)
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

        services.RegisterSharedPipelineServices(configuration);

        services.AddQuartz(opt =>
        {
            opt.UseMicrosoftDependencyInjectionJobFactory();

            opt.AddJob<TestJob>(jobOpt => jobOpt.WithIdentity(TestJob.Key));

            opt.AddTrigger(
                triggerOpt =>
                    triggerOpt
                        .ForJob(TestJob.Key)
                        .WithSimpleSchedule(
                            scheduleOpt => scheduleOpt.WithIntervalInSeconds(5).RepeatForever()
                        )
            );

            opt.UseInMemoryStore();
        });

        services.AddQuartzHostedService(opt =>
        {
            opt.AwaitApplicationStarted = true;
        });

        return services;
    }

    public static ConfigureHostBuilder RegisterSchedulerHost(this ConfigureHostBuilder host)
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
                        path: "logs/Scheduler.log",
                        rollingInterval: RollingInterval.Day,
                        levelSwitch: new LoggingLevelSwitch(LogEventLevel.Debug),
                        formatProvider: new CultureInfo("en-US")
                    );
                });

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
}
