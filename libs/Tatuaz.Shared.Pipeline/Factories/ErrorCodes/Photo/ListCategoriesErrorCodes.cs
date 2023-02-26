namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Photo;

public class ListCategoriesErrorCodes
{
    public const string PageNumberIsLessThan1 = "PageNumberIsLessThan1";
    public const string PageSizeIsLessThan1 = "PageSizeIsLessThan1";
    public const string PageSizeIsGreaterThan1000 = "PageSizeIsGreaterThan1000";
    public static string PageSizeIsNull = "PageSizeIsNull";
    public static string PageNumberIsNull = "PageNumberIsNull";
}