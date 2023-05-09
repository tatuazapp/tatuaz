using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;

[NoUndef]
public record CategoryDto(int Id, string Title, CategoryTypeDto Type, string ImageUri) : IDto;
