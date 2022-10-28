using System.Net;
using MassTransit;
using Tatuaz.Shared.Pipeline.Factories.Errors;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Consumers;

public abstract class TatuazConsumerBase<TMessage, TData> : IConsumer<TMessage>
    where TMessage : class
{
    public async Task Consume(ConsumeContext<TMessage> context)
    {
        try
        {
            await context.RespondAsync(ConsumeMessage(context.Message)).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
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
