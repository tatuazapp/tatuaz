namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

public static class SignUpErrorCodes
{
    public const string UserAlreadyExists = "UserAlreadyExists";
    public const string UsernameNull = "UsernameNull";
    public const string UsernameTooShort = "UsernameTooShort";
    public const string UsernameTooLong = "UsernameTooLong";
    public const string UsernameAlreadyInUse = "UsernameAlreadyInUse";
    public const string UsernameInvalidCharacters = "UsernameInvalidCharacters";
    public const string CategoryIdsTooFew = "CategoryIdsTooFew";
    public const string CategoryIdsTooMany = "CategoryIdsTooMany";
    public const string CategoryIdsInvalid = "CategoryIdsInvalid";
    public const string CategoryIdsDuplicate = "CategoryIdsDuplicate";
    public const string CategoryIdsNull = "CategoryIdsNull";
}
