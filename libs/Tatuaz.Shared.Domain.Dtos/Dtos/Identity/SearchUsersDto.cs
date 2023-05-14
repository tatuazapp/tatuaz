namespace Tatuaz.Shared.Domain.Dtos.Dtos.Identity;

public record SearchUsersDto(string? Query, int? PageNumber, int? PageSize, bool? OnlyArtists);
