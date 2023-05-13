using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using Azure.Storage.Blobs;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Tatuaz.Dashboard.Emails;
using Tatuaz.Dashboard.Emails.Configuration;
using Tatuaz.Dashboard.Emails.Exceptions;
using Tatuaz.Dashboard.Queue;
using Tatuaz.Dashboard.Queue.Consumers.Emails;
using Tatuaz.Dashboard.Queue.Consumers.Statistics;
using Tatuaz.Dashboard.Queue.Contracts.Emails;
using Tatuaz.Shared.Domain.Dtos;
using Tatuaz.Shared.Helpers;
using Tatuaz.Shared.Infrastructure;
using Tatuaz.Shared.Infrastructure.DataAccess;
using Tatuaz.Shared.Pipeline;
using Tatuaz.Shared.Pipeline.Configuration;

namespace Tatuaz.Dashboard;

public static class DashboardExtensions
{
    public static IServiceCollection RegisterDashboardServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.RegisterDashboardQueueServices();

        services.RegisterSharedInfrastructureServices<MainDbContext>(
            configuration.GetConnectionString(
                SharedInfrastructureExtensions.MainDbConnectionStringName
            ) ?? throw new Exception("Connection string not found")
        );

        services.RegisterSharedDomainDtosServices();

        services.RegisterSharedPipelineServices(
            configuration,
            new[] { typeof(SendEmail).Assembly },
            (_, config) =>
            {
                config.UseMessageRetry(r =>
                {
                    r.Handle<SendEmailException>();
                    r.Incremental(5, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
                });
            }
        );

        var emailOpt =
            configuration.GetRequiredSection(EmailOpt.SectionName).Get<EmailOpt>()
            ?? throw new Exception("Email configuration is missing");

        // Change for something else in production
        services
            .AddFluentEmail(emailOpt.FromEmail, emailOpt.FromName)
            .AddLiquidRenderer(opt =>
            {
                opt.FileProvider = new EmbeddedFileProvider(typeof(EmailType).Assembly);
            })
            .AddSmtpSender(
                new SmtpClient
                {
                    Host = emailOpt.SmtpHost,
                    Port = emailOpt.SmtpPort,
                    Credentials = new NetworkCredential
                    {
                        UserName = emailOpt.SmtpUsername,
                        Password = emailOpt.SmtpPassword
                    }
                }
            );

        services.AddScoped<IEmailHandlerFactory, EmailHandlerFactory>();
        var blobOpt = GetBlobOpt(configuration);
        services.AddScoped(
            _ => new BlobContainerClient(blobOpt.ConnectionString, blobOpt.ImagesContainerName)
        );

        return services;
    }

    public static IHostBuilder RegisterDashboardHost(
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
                                levelSwitch: new LoggingLevelSwitch(LogEventLevel.Debug),
                                formatProvider: new CultureInfo("en-US")
                            )
                    );

                    loggerConfiguration.WriteTo.Async(x =>
                    {
                        x.File(
                            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                            path: "logs/dashboard.log",
                            rollingInterval: RollingInterval.Day,
                            levelSwitch: new LoggingLevelSwitch(LogEventLevel.Debug),
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

    public static BlobOpt GetBlobOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(BlobOpt.SectionName).Get<BlobOpt>()
            ?? throw new Exception("Blob options not found");
    }
}
