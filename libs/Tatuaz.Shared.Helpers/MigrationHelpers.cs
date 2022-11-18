using System;
using System.IO;

namespace Tatuaz.Shared.Helpers;

public static class MigrationHelpers
{
    public static string ReadSql(Type migrationType, string sqlFileName)
    {
        var assembly = migrationType.Assembly;
        var resourceName = $"{migrationType.Namespace}.{sqlFileName}";
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new FileNotFoundException(
                "Unable to find the SQL file from an embedded resource",
                resourceName
            );
        }

        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd();
        return content;
    }
}
