using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Tatuaz.History.DataAccess.Exceptions;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.Queue.Consumers;

public class DumpHistoryConsumer : IConsumer<DumpHistoryOrder>
{
    private readonly ILogger<DumpHistoryConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DumpHistoryConsumer(
        ILogger<DumpHistoryConsumer> logger,
        IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task Consume(ConsumeContext<DumpHistoryOrder> context)
    {
        _logger.LogInformation("DumpHistoryConsumer: {0}", context.Message);

        var (toDump, type) = Deserialize(context.Message.Object, context.Message.ObjectType);

        var histId = await GetDumpTask(toDump, type).ConfigureAwait(false);

        _logger.LogInformation(
            "Dumped history for {ObjectType} with HistId {Id}",
            context.Message.ObjectType,
            histId
        );
    }

    private static (object toDump, Type type) Deserialize(string objectJson, string typeName)
    {
        var jsonSerializerSettings = new JsonSerializerSettings();
        jsonSerializerSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

        var type = typeof(HistEntity<>).Assembly.GetType(typeName);
        if (type is null)
        {
            throw new HistException(
                "Type to deserialize doesn't derive from HistEntity<> or is not in the same assembly."
            );
        }

        var toDump = JsonConvert.DeserializeObject(objectJson, type, jsonSerializerSettings);
        if (toDump is null)
        {
            throw new HistException($"Cannot deserialize object of type {type.Name} to dump.");
        }

        return (toDump, type);
    }

    private Task<Guid> GetDumpTask(object toDump, Type type)
    {
        var idType = toDump.GetType().GetProperty("Id")?.PropertyType;
        if (idType is null)
        {
            throw new HistException($"Cannot find Id property in type {type.Name}.");
        }

        var serviceType = typeof(IDumpHistoryService<,>).MakeGenericType(toDump.GetType(), idType);
        var service = _serviceProvider.GetRequiredService(serviceType);

        var dumpAsyncMethodInfo = service.GetType().GetMethod("DumpAsync");
        if (dumpAsyncMethodInfo is null)
        {
            throw new HistException(
                $"Cannot find required DumpAsync method in service {serviceType.Name}."
            );
        }

        if (dumpAsyncMethodInfo.Invoke(service, new[] { toDump }) is not Task<Guid> dumpTask)
        {
            throw new HistException(
                $"Cannot invoke DumpAsync method in service {serviceType.Name}."
            );
        }

        return dumpTask;
    }
}
