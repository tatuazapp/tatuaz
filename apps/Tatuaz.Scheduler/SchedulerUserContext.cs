using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Scheduler;

public class SchedulerUserContext : IUserContext
{
    public string? CurrentUserEmail { get; set; } = "SchedulerMicroservice@tatuaz.app";
    public string? CurrentUserAuth0Id { get; set; } = "SchedulerMicroservice";
}
