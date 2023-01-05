using System;
using System.Linq;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Configuration;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Shared.Pipeline;

public static class SharedPipelineExtensions
{
    public static IServiceCollection RegisterSharedPipelineServices(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies
    )
    {
        var rabbitMqOpt = configuration.GetRabbitMqOpt();
        services.AddMassTransit(x =>
        {
            if (assemblies.Any())
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
                    cfg.ConfigureEndpoints(context);
                }
            );
        });
        services.AddScoped<IUserContext, InternalUserContext>();

        return services;
    }

    public static RabbitMqOpt GetRabbitMqOpt(this IConfiguration configuration)
    {
        return configuration.GetSection(RabbitMqOpt.SectionName).Get<RabbitMqOpt>()
            ?? throw new Exception("RabbitMq configuration is missing");
    }
}
