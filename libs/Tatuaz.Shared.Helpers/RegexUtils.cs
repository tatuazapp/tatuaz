using System.Text.RegularExpressions;

namespace Tatuaz.Shared.Helpers;

public static class RegexUtils
{
    public static Regex PhoneNumberRegex => new("^\\+?[1-9][0-9]{7,14}$");
}
