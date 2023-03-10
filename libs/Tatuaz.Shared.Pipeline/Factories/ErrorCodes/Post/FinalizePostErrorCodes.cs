namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

public class FinalizePostErrorCodes
{
    public const string DescriptionIsNull = "DescriptionIsNull";
    public const string DescriptionIsTooLong = "DescriptionIsTooLong";
    public const string InitialPostDoesNotExist = "InitialPostDoesNotExist";
    public const string PhotoNotFoundOnInitialPost = "PhotoNotFoundOnInitialPost";
    public const string PhotoMissing = "PhotoMissing";
    public const string UserIsNotTheAuthorOfTheInitialPost = "UserIsNotTheAuthorOfTheInitialPost";
}
