using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;

public record ListSummaryStatsDto(SummaryStatTimePeriod? TimePeriod, int? Count) : IDto;
