using System;

namespace Tatuaz.History.Queue;

public static class HistoryQueueConstants
{
    public const string DumpQueueName = "DumpHistory";
    public static readonly Uri DumpQueueUri = new($"queue:{DumpQueueName}");
}
