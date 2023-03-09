using System.IO;
using Microsoft.AspNetCore.Http;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;

public record SetForegroundPhotoDto(IFormFile? Photo) : IDto;
