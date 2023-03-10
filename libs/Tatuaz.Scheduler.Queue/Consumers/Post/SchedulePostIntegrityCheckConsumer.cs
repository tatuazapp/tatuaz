using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using Tatuaz.Scheduler.Queue.Consumers.Common;
using Tatuaz.Scheduler.Queue.Contracts.Post;

namespace Tatuaz.Scheduler.Queue.Consumers.Post;

public class SchedulePostIntegrityCheckConsumer : TriggerSchedulingJob<SchedulePostIntegrityCheck>
{
    public static readonly JobKey Key = new("PostIntegrityCheck", "Post");
    private const int MinutesToFire = 1;

    public SchedulePostIntegrityCheckConsumer(ISchedulerFactory schedulerFactory, ILogger<SchedulePostIntegrityCheckConsumer> logger) : base(schedulerFactory, logger)
    {
    }

    protected override Func<ConsumeContext<SchedulePostIntegrityCheck>, CancellationToken, Task<ITrigger>> CreateTrigger
        => (context, _) => Task.FromResult(TriggerBuilder.Create()
            .StartAt(DateBuilder.FutureDate(MinutesToFire, IntervalUnit.Minute))
            .UsingJobData(new JobDataMap(context.Message.PhotoIds.ToDictionary(v => v.ToString("N"))))
            .ForJob(Key)
            .Build());
}
