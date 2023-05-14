namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

public static class SearchUsersErrorCodes
{
    public const string QueryNull = "QueryNull";
    public const string PageNumberLessThan1 = "PageNumberLessThan1";
    public const string PageSizeLessThan1 = "PageSizeLessThan1";
    public const string PageSizeGreaterThan1000 = "PageSizeGreaterThan1000";
    public const string OnlyArtistsNull = "OnlyArtistsNull";
}
