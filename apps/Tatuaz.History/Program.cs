using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tatuaz.History.DataAccess;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue;
using Tatuaz.History.Queue.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<DumpHistoryConsumer>();

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
                    e.ConfigureConsumer(context, typeof(HistCountByPredicateConsumer<,>));
                    e.ConfigureConsumer(context, typeof(HistExistsByIdConsumer<,>));
                    e.ConfigureConsumer(context, typeof(HistExistsByPredicateConsumer<,>));
                    e.ConfigureConsumer(context, typeof(HistGetByIdConsumer<,>));
                    e.ConfigureConsumer(context, typeof(HistGetByPredicateConsumer<,>));
                    e.ConfigureConsumer(context, typeof(HistGetByPredicateWithPagingConsumer<,>));
                }
            );
        }
    );
});

builder.Services.AddScoped(typeof(IDumpHistoryService<,>), typeof(DumpHistoryService<,>));
builder.Services.AddDbContext<HistDbContext>(
    opt =>
        opt.UseNpgsql(
            builder.Configuration.GetConnectionString("TatuazHistory"),
            npgsqlOpt =>
            {
                npgsqlOpt.EnableRetryOnFailure(5);
                npgsqlOpt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }
        )
);

builder.Build().Run();
