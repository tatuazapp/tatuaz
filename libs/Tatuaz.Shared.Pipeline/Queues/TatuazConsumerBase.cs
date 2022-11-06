using System;
using System.Net;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Errors;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Queues;

public abstract class TatuazConsumerBase<TMessage, TData> : IConsumer<TMessage>
    where TMessage : TatuazMessage
{
    private readonly ILogger _logger;

    public TatuazConsumerBase(ILogger logger)
    {
        _logger = logger;
    }

    protected IUserAccessor UserAccessor { get; private set; }

    public async Task Consume(ConsumeContext<TMessage> context)
    {
        _logger.LogInformation("Received message {MessageId} of type {MessageType} in {ClassName}", context.MessageId,
            typeof(TMessage).Name, GetType().Name);
        try
        {
            UserAccessor = new InternalUserAccessor(context.Message.UserId);
            await context.RespondAsync(await ConsumeMessage(context.Message).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            _logger.LogError("Error while processing message {MessageId} of type {MessageType}: {Exception}",
                context.MessageId, typeof(TMessage).Name, exception);
            var (errors, httpStatusCode) =
                await HandleException(context, exception).ConfigureAwait(false);
            await context
                .RespondAsync(
                    new TatuazResult<TData>(errors, httpStatusCode)).ConfigureAwait(false);
        }
    }

    protected abstract Task<TatuazResult<TData>> ConsumeMessage(TMessage message);

    protected virtual Task<(TatuazError[] errors, HttpStatusCode httpStatusCode)> HandleException(
        ConsumeContext<TMessage> context, Exception exception)
    {
        return Task.FromResult((new[] { CommonErrorFactory.InternalError() }, HttpStatusCode.InternalServerError));
    }
}
