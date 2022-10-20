using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.History.DataAccess;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue;
using Tatuaz.History.Queue.Consumers;

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
                        e =>
                        {
                            e.ConfigureConsumer<DumpHistoryConsumer>(context);
                        }
                    );
                    cfg.ReceiveEndpoint(
                        HistoryQueueConstants.QueryQueueName,
                        e =>
                        {
                            e.ConfigureConsumer<Test1Consumer>(context);
                        }
                    );
                }
            );
        });

        return services;
    }

    public static IServiceCollection AddHistoryDatabaseProvider(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped(typeof(IDumpHistoryService<,>), typeof(DumpHistoryService<,>));
        services.AddScoped(typeof(IHistorySearcherService<,>), typeof(HistorySearcherService<,>));
        services.AddDbContext<HistDbContext>(opt =>
        {
            opt.UseNpgsql(
                configuration.GetConnectionString("TatuazHistory"),
                npgsqlOpt =>
                {
                    npgsqlOpt.EnableRetryOnFailure(5);
                    npgsqlOpt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }
            );
        });
        return services;
    }
}
