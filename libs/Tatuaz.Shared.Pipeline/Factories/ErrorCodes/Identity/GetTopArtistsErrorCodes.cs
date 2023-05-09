namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

public static class GetTopArtistsErrorCodes
{
    public const string PageNumberIsLessThan1 = "PageNumberIsLessThan1";
    public const string PageSizeIsLessThan1 = "PageSizeIsLessThan1";
    public const string PageSizeIsGreaterThan1000 = "PageSizeIsGreaterThan1000";
    public const string PageSizeIsNull = "PageSizeIsNull";
    public const string PageNumberIsNull = "PageNumberIsNull";
}
