using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;

public record PhotoCategoryDto(
    int Id,
    string Title,
    PhotoCategoryTypeDto Type,
    string ImageUri,
    int Popularity
) : IDto;
