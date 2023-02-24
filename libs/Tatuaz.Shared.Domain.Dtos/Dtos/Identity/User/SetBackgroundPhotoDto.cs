using System.IO;
using Microsoft.AspNetCore.Http;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;

public record SetBackgroundPhotoDto(IFormFile? Photo);
