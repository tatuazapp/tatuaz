using MassTransit;
using Microsoft.AspNetCore.Builder;
using Tatuaz.History.Queue.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<DumpHistoryConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint(DumpHistoryConsumer.QueueName, e => { e.ConfigureConsumer<DumpHistoryConsumer>(context); });
    });
});

builder.Build().Run();