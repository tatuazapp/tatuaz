using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;

namespace Tatuaz.Shared.Helpers;

public static class SerializationUtils
{
    public static JsonSerializerSettings GetTatuazSerializerSettings()
    {
        var jsonSerializerSettings = new JsonSerializerSettings();
        jsonSerializerSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        return jsonSerializerSettings;
    }
}
