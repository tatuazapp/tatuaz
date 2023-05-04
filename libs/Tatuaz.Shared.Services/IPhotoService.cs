using System;
using System.IO;
using System.Threading.Tasks;

namespace Tatuaz.Shared.Services;

public interface IPhotoService
{
    bool ValidatePhotoHeaders(Stream stream);
}
