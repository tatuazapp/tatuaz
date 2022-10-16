using System.Text.Json;

using MassTransit;

using Microsoft.Extensions.Logging;

using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class DumpHistoryConsumer : IConsumer<DumpHistoryOrder>
{
    private readonly ILogger<DumpHistoryConsumer> _logger;

    public static string QueueName = "dump-history";
    public static Uri Uri = new Uri($"queue:{QueueName}");

    public DumpHistoryConsumer(ILogger<DumpHistoryConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DumpHistoryOrder> context)
    {
        var assembly = typeof(HistEntity<>).Assembly;
        var type = assembly.GetType(context.Message.ObjectType);

        _logger.LogInformation(context.Message.Data);
    }
}
