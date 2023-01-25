using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Tatuaz.TatuazSchedulerJobs;

public class TestJob : IJob
{
    public static readonly JobKey Key = new("TestJob", "TestGroup");

    private readonly ILogger<TestJob> _logger;

    public TestJob(ILogger<TestJob> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Running TestJob");
        return Task.CompletedTask;
    }
}
