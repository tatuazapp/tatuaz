using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

public record GetTopArtistsDto(int? PageNumber, int? PageSize) : IDto;
