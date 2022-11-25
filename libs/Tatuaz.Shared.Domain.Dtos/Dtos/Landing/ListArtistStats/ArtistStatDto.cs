using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;

public record ArtistStatDto(
    string Username,
    string? BackgroundUrl,
    string RedirectUrl,
    EngagementStatDto EngagementStat
) : IDto;
