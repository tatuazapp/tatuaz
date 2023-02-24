using System.IO;

namespace Tatuaz.Shared.Services;

public interface IPhotoService
{
    bool ValidatePhotoHeaders(Stream stream);
}
