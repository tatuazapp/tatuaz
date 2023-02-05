namespace Tatuaz.Gateway.Configuration;

public class BlobOpt
{
    public const string SectionName = "Blob";

    public string ConnectionString { get; set; } = default!;
    public string ImagesContainerName { get; set; } = default!;
    public string ImagesCacheContainerName { get; set; } = default!;
}
