namespace Tatuaz.History.Queue;

public static class HistoryQueueConstants
{
    public const string DumpQueueName = "dump-history";
    public const string QueryQueueName = "query-history";
    public static readonly Uri DumpQueueUri = new($"queue:{DumpQueueName}");
    public static readonly Uri QueryQueueUri = new($"queue:{QueryQueueName}");
}
