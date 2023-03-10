using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Tatuaz.TatuazSchedulerJobs.Post;

public class PostIntegrityCheckJob : IJob
{
    private readonly ILogger<PostIntegrityCheckJob> _logger;
    public static readonly JobKey Key = new("PostIntegrityCheck", "Post");

    public PostIntegrityCheckJob(
        ILogger<PostIntegrityCheckJob> logger
        )
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("PostIntegrityCheckJob fired.");

        return Task.CompletedTask;
    }
}
