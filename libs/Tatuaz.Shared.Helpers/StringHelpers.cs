using Serilog.Events;

namespace Tatuaz.Shared.Helpers;

public static class StringHelpers
{
    public static LogEventLevel GetLoggingLevelSwitch(string level)
    {
        return level switch
        {
            "Verbose" => LogEventLevel.Verbose,
            "Debug" => LogEventLevel.Debug,
            "Information" => LogEventLevel.Information,
            "Warning" => LogEventLevel.Warning,
            "Error" => LogEventLevel.Error,
            "Fatal" => LogEventLevel.Fatal,
            _ => LogEventLevel.Information
        };
    }
}
