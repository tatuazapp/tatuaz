using System;

namespace Tatuaz.Dashboard.Queue;

public static class DashboardQueueConstants
{
    public const string SendEmailQueueName = "SendEmail";
    public static readonly Uri SendEmailQueueUri = new($"queue:{SendEmailQueueName}");
}
