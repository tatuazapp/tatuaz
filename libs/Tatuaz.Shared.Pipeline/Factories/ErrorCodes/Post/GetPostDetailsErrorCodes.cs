namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

public static class GetPostDetailsErrorCodes
{
    public const string PostIdIsNull = "PostIdIsNull";
    public const string PostDoesNotExist = "PostDoesNotExist";

    public const string PageSizeIsLessThan1 = "PageSizeIsLessThan1";
    public const string PageSizeIsGreaterThan1000 = "PageSizeIsGreaterThan1000";
    public const string PageNumberIsNull = "PageNumberIsNull";
    public const string PageNumberIsLessThan1 = "PageNumberIsLessThan1";
}
