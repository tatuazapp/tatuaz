using System.IO;

namespace Tatuaz.Shared.Helpers;

public static class StreamHelpers
{
    public static byte[] FullyReadStream(this Stream stream)
    {
        var buffer = new byte[16 * 1024];
        using var ms = new MemoryStream();
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            ms.Write(buffer, 0, read);
        }

        return ms.ToArray();
    }
}
