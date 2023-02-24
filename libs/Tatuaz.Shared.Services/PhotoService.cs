using System.Globalization;
using System.IO;
using System.Linq;

namespace Tatuaz.Shared.Services;

public class PhotoService : IPhotoService
{
    public bool ValidatePhotoHeaders(Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        var buffer = new byte[4];
        var read = stream.Read(buffer, 0, 4);
        if(read != 4)
            return false;
        var hex = string.Join(string.Empty, buffer.Select(x => x.ToString("X2", new CultureInfo("en-US"))));

        var isJpg = hex == "FFD8FF";
        var isPng = hex == "89504E47";
        var isWebp = hex == "52494646";

        return isJpg || isPng || isWebp;
    }
}
