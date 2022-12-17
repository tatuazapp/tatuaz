using System;
using System.Net;
using System.Reflection;
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
    protected IUserContext? UserContext { get; private set; }

    public TatuazConsumerBase(ILogger logger)
    {
        _logger = logger;
    }

    protected abstract Task<TatuazResult<TData>> ConsumeMessage(TMessage message);

    public async Task Consume(ConsumeContext<TMessage> context)
    {
        UserContext = new InternalUserContext(context.Message.UserId);
        SetUserContext(UserContext);
        _logger.LogInformation(
            "Received message {MessageId} of type {MessageType} in {ClassName}",
            context.MessageId,
            typeof(TMessage).Name,
            GetType().Name
        );
        try
        {
            await context
                .RespondAsync(await ConsumeMessage(context.Message).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                "Error while processing message {MessageId} of type {MessageType}: {Exception}",
                context.MessageId,
                typeof(TMessage).Name,
                exception
            );
            var (errors, httpStatusCode) = await HandleException(context, exception)
                .ConfigureAwait(false);
            await context
                .RespondAsync(new TatuazResult<TData>(errors, httpStatusCode))
                .ConfigureAwait(false);
        }
    }

    protected virtual Task<(TatuazError[] errors, HttpStatusCode httpStatusCode)> HandleException(
        ConsumeContext<TMessage> context,
        Exception exception
    )
    {
        return Task.FromResult(
            (new[] { CommonErrorFactory.InternalError() }, HttpStatusCode.InternalServerError)
        );
    }

    private void SetUserContext(IUserContext userContext)
    {
        const BindingFlags bindingFlags =
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
        var fields = GetType().GetFields(bindingFlags);
        foreach (var field in fields)
        {
            if (field.GetValue(this) is IUserContextEnjoyer userContextEnjoyer)
            {
                userContextEnjoyer.SetUserContext(userContext);
            }
        }

        var properties = GetType().GetProperties(bindingFlags);
        foreach (var property in properties)
        {
            if (property.GetValue(this) is IUserContextEnjoyer userContextEnjoyer)
            {
                userContextEnjoyer.SetUserContext(userContext);
            }
        }
    }
}
