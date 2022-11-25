using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;

public record EngagementStatDto(EngagementStatType StatType, string Value) : IDto;
