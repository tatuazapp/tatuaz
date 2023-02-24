namespace Tatuaz.Shared.Pipeline.Configuration;

public class SerilogOpt
{
    public static string SectionName = "Serilog";
    public string BlobConnectionString { get; set; } = default!;
    public string BlobContainerName { get; set; } = default!;
    public string CloudLogLevel { get; set; } = default!;
    public string FileLogLevel { get; set; } = default!;
    public string ConsoleLogLevel { get; set; } = default!;
    public string BlobFileName { get; set; } = default!;
}
