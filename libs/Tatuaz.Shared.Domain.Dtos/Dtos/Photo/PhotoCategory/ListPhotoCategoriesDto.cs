using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;

public record ListPhotoCategoriesDto(int PageNumber, int PageSize) : IDto;
