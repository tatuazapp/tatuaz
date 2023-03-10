using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Tatuaz.Scheduler.Queue.Consumers.Common;

public abstract class TriggerSchedulingJob<T> : IConsumer<T>
    where T : class
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly ILogger _logger;
    protected abstract Func<ConsumeContext<T>,CancellationToken,Task<ITrigger>> CreateTrigger { get; }

    public TriggerSchedulingJob(
        ISchedulerFactory schedulerFactory,
        ILogger logger
        )
    {
        _schedulerFactory = schedulerFactory;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<T> context)
    {
        var scheduler = await _schedulerFactory.GetScheduler().ConfigureAwait(false);
        var trigger = await CreateTrigger(context, context.CancellationToken).ConfigureAwait(false);
        await scheduler.ScheduleJob(trigger).ConfigureAwait(false);
        _logger.LogInformation("Trigger with name {TriggerName} scheduled at {FirstFireTime} UTC.", trigger.JobKey.Name, trigger.StartTimeUtc);
    }
}
