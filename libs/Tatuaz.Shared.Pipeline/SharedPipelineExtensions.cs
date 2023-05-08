using System;
using System.Linq;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IO;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Configuration;
using Tatuaz.Shared.Pipeline.Filters;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Shared.Pipeline;

public static class SharedPipelineExtensions
{
    public static IServiceCollection RegisterSharedPipelineServices(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly[]? assemblies = null,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? configure = null
    )
    {
        var rabbitMqOpt = configuration.GetRabbitMqOpt();
        services.AddMassTransit(x =>
        {
            if (assemblies != null && assemblies.Any())
            {
                x.AddConsumers(assemblies);
            }

            x.UsingRabbitMq(
                (context, cfg) =>
                {
                    cfg.Host(
                        rabbitMqOpt.Host,
                        rabbitMqOpt.VirtualHost,
                        h =>
                        {
                            h.Username(rabbitMqOpt.Username);
                            h.Password(rabbitMqOpt.Password);
                        }
                    );
                    cfg.UseSendFilter(typeof(UserContextSendFilter<>), context);
                    cfg.UsePublishFilter(typeof(UserContextPublishFilter<>), context);
                    cfg.UseConsumeFilter(typeof(UserContextConsumeFilter<>), context);
                    configure?.Invoke(context, cfg);
                    cfg.ConfigureEndpoints(context);
                }
            );
        });
        services.AddScoped<UserContextActionFilter>();
        services.AddScoped<IUserContext, UserContext.UserContext>();
        services.AddSingleton(
            new RecyclableMemoryStreamManager(1024, 1024 * 1024, 32 * 1024 * 1024)
        );

        return services;
    }

    public static RabbitMqOpt GetRabbitMqOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(RabbitMqOpt.SectionName).Get<RabbitMqOpt>()
            ?? throw new Exception("RabbitMq configuration is missing");
    }
}
