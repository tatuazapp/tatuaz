using System;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Post;

public record UploadedPhotosDto(Guid InitialPostId, Guid[] Photos) : IDto;
