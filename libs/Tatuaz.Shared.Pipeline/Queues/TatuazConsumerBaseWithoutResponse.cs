using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Shared.Helpers;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Queues;

public abstract class TatuazConsumerBaseWithoutResponse<TMessage> : IConsumer<TMessage>
    where TMessage : class
{
    private readonly ILogger _logger;

    public TatuazConsumerBaseWithoutResponse(ILogger logger)
    {
        _logger = logger;
    }

    protected abstract Task ConsumeMessage(ConsumeContext<TMessage> context);

    public async Task Consume(ConsumeContext<TMessage> context)
    {
        _logger.LogInformation(
            "Received message {MessageId} of type {MessageType} in {ClassName}",
            context.MessageId.ToString(),
            typeof(TMessage).Name,
            GetType().Name
        );
        try
        {
            await ConsumeMessage(context).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                "Error while processing message {MessageId} of type {MessageType}: {Exception}",
                context.MessageId.ToString(),
                typeof(TMessage).Name,
                exception
            );
            await HandleException(context, exception).ConfigureAwait(false);
        }
    }

    protected virtual Task HandleException(ConsumeContext<TMessage> context, Exception exception)
    {
        return Task.CompletedTask;
    }
}
