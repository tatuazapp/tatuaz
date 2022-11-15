namespace Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Identity;

public class CreateUserErrorCodes
{
    public const string UserAlreadyExists = "UserAlreadyExists";

    public const string EmailNull = "EmailNull";
    public const string EmailInvalid = "EmailInvalid";
    public const string EmailEmpty = "EmailEmpty";
    public const string EmailTooLong = "EmailTooLong";
    public const string EmailAlreadyExists = "EmailAlreadyExists";

    public const string UsernameNull = "UsernameNull";
    public const string UsernameTooShort = "UsernameTooShort";
    public const string UsernameTooLong = "UsernameTooLong";
    public const string UsernameAlreadyExists = "UsernameAlreadyExists";

    public const string PhoneNumberInvalid = "PhoneNumberInvalid";
}
