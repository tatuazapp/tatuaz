using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Tatuaz.History.DataAccess.Exceptions;
using Tatuaz.History.DataAccess.Services;
using Tatuaz.History.Queue.Contracts;
using Tatuaz.History.Queue.Util;
using Tatuaz.Shared.Domain.Entities.Hist.Common;
using Tatuaz.Shared.Helpers;

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
        _logger.LogInformation("DumpHistoryConsumer: {Message}", context.Message);

        var toDump = HistorySerializer.DeserializeDumpHistoryOrder(context.Message);

        var histId = await GetDumpTask(toDump, toDump.GetType(), context.CancellationToken)
            .ConfigureAwait(false);

        _logger.LogInformation(
            "Dumped history for {ObjectType} with HistId {Id}",
            context.Message.ObjectType,
            histId
        );
    }

    private Task<Guid> GetDumpTask(
        HistEntity toDump,
        MemberInfo type,
        CancellationToken cancellationToken = default
    )
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

        if (
            dumpAsyncMethodInfo.Invoke(service, new object[] { toDump, cancellationToken })
            is not Task<Guid> dumpTask
        )
        {
            throw new HistException(
                $"Cannot invoke DumpAsync method in service {serviceType.Name}."
            );
        }

        return dumpTask;
    }
}
