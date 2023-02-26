using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;

public record ListCategoriesDto(int? PageNumber, int? PageSize) : IDto;
