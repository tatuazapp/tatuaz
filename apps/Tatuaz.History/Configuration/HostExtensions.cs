using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Tatuaz.History.Configuration;

public static class HostExtensions
{
    public static ConfigureHostBuilder AddHistoryLogging(this ConfigureHostBuilder host)
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
                        path: "logs/history.log",
                        rollingInterval: RollingInterval.Day,
                        levelSwitch: new LoggingLevelSwitch(LogEventLevel.Debug)
                    );
                });

                loggerConfiguration.WriteTo.Async(x =>
                {
                    x.File(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                        path: "logs/history_error.log",
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
}
