using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using NodaTime;
using Tatuaz.Dashboard.Emails;
using Tatuaz.Dashboard.Queue.Contracts.Emails;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Emails;

public class SendEmailConsumer : TatuazConsumerBaseWithoutResponse<SendEmailOrder>
{
    private readonly ILogger<SendEmailConsumer> _logger;
    private readonly IEmailHandlerFactory _emailHandlerFactory;
    private readonly IClock _clock;

    public SendEmailConsumer(
        ILogger<SendEmailConsumer> logger,
        IEmailHandlerFactory emailHandlerFactory,
        IClock clock
    ) : base(logger)
    {
        _logger = logger;
        _emailHandlerFactory = emailHandlerFactory;
        _clock = clock;
    }

    protected override async Task ConsumeMessage(ConsumeContext<SendEmailOrder> context)
    {
        _logger.LogInformation(
            "Sending email of type {EmailType} to {RecipientEmail} about {ObjectId}",
            Enum.GetName(context.Message.EmailType),
            context.Message.RecipientEmail,
            context.Message.ObjectId.ToString()
        );

        await _emailHandlerFactory
            .GetEmailHandler(context.Message.EmailType)
            .SendEmailAsync(
                context.Message.EmailId,
                context.Message.RecipientEmail,
                context.Message.ObjectId
            )
            .ConfigureAwait(false);

        await context
            .Publish(new EmailSent(context.Message.EmailId, _clock.GetCurrentInstant()))
            .ConfigureAwait(false);
    }
}
