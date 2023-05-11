namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

public class SearchPostsErrorCodes
{
    public const string QueryNull = "QueryNull";
    public const string PageSizeIsLessThan1 = "PageSizeIsLessThan1";
    public const string PageSizeIsGreaterThan1000 = "PageSizeIsGreaterThan1000";
    public const string PageNumberIsNull = "PageNumberIsNull";
    public const string PageNumberIsLessThan1 = "PageNumberIsLessThan1";
    public const string QueryTooLong = "QueryTooLong";
    public const string SearchPostsFlagIsNull = "SearchPostsFlagIsNull";
}
