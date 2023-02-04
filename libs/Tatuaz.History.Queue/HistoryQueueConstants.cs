using System;

namespace Tatuaz.History.Queue;

public static class HistoryQueueConstants
{
    public const string DumpHistoryQueueName = "DumpHistory";
    public static readonly Uri DumpHistoryQueueUri = new($"queue:{DumpHistoryQueueName}");
}
