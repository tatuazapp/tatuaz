using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.History.Queue;
using Tatuaz.History.Queue.Consumers;
using Tatuaz.History.Queue.Consumers.Common;

namespace Tatuaz.History.Configuration;

public static class ServiceExtensions
{
    public static IServiceCollection AddHistoryQueue(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(typeof(DumpHistoryConsumer).Assembly);
            x.UsingRabbitMq(
                (context, cfg) =>
                {
                    cfg.Host(
                        "localhost",
                        "/",
                        h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        }
                    );

                    cfg.ReceiveEndpoint(
                        HistoryQueueConstants.DumpQueueName,
                        e => { e.ConfigureConsumer<DumpHistoryConsumer>(context); }
                    );
                    cfg.ReceiveEndpoint(
                        HistoryQueueConstants.QueryQueueName,
                        e => { e.ConfigureConsumer<Test1Consumer>(context); }
                    );
                }
            );
        });

        return services;
    }
}
