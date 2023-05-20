using Microsoft.AspNetCore.Http;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

public record SetForegroundPhotoDto(IFormFile? Photo) : IDto;
