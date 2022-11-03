using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.Enums;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Landing;

public record ListStatsDto
(
    StatsTimePeriod TimePeriod,
    int Count
);