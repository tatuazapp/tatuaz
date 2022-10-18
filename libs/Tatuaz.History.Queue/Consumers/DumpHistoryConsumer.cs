using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class DumpHistoryConsumer : IConsumer<DumpHistoryOrder>
{
    public const string QueueName = "dump-history";
    public static readonly Uri Uri = new($"queue:{QueueName}");
    private readonly ILogger<DumpHistoryConsumer> _logger;

    public DumpHistoryConsumer(ILogger<DumpHistoryConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DumpHistoryOrder> context)
    {
        var assembly = typeof(HistEntity<>).Assembly;
        _ = assembly.GetType(context.Message.ObjectType);

        _logger.LogInformation(context.Message.Data);
    }
}