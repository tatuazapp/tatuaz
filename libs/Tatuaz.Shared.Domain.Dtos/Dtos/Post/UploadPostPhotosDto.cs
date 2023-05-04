using Microsoft.AspNetCore.Http;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post;

public record UploadPostPhotosDto(IFormFile[] Photos) : IDto;
