namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

public static class GetUserPostsErrorCodes
{
    public const string UsernameIsNull = "UsernameIsNull";
    public const string PageSizeIsLessThan1 = "PageSizeIsLessThan1";
    public const string PageSizeIsGreaterThan1000 = "PageSizeIsGreaterThan1000";
    public const string PageNumberIsNull = "PageNumberIsNull";
    public const string PageNumberIsLessThan1 = "PageNumberIsLessThan1";
    public const string PageSizeIsNull = "PageSizeIsNull";
    public const string UserNotFound = "UserNotFound";
}
