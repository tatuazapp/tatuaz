namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

public class CreateUserErrorCodes
{
    public const string UserAlreadyExists = "UserAlreadyExists";

    public const string UsernameNull = "UsernameNull";
    public const string UsernameTooShort = "UsernameTooShort";
    public const string UsernameTooLong = "UsernameTooLong";
    public const string UsernameAlreadyInUse = "UsernameAlreadyInUse";
    public static string UsernameInvalidCharacters = "UsernameInvalidCharacters";
    public static string PhotoCategoryIdsTooFew = "PhotoCategoryIdsTooFew";
    public static string PhotoCategoryIdsTooMany = "PhotoCategoryIdsTooMany";
    public static string PhotoCategoryIdsInvalid = "PhotoCategoryIdsInvalid";
    public static string PhotoCategoryIdsDuplicate = "PhotoCategoryIdsDuplicate";
    public static string PhotoCategoryIdsNull = "PhotoCategoryIdsNull";
}
