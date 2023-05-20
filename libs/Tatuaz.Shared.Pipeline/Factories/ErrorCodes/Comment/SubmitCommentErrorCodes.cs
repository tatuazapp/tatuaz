namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Comment;

public class SubmitCommentErrorCodes
{
    public const string ContentIsNull = "ContentIsNull";
    public const string ContentIsTooLong = "ContentIsTooLong";

    public const string PostIsNull = "PostIsNull";
    public const string PostNotFound = "PostNotFound";

    public const string ParentCommentNotFound = "ParentCommentNotFound";
}
