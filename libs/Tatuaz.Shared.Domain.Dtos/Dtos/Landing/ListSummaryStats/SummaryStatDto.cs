using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;

public record SummaryStatDto(string Title, string Content, string BackgroundUrl) : IDto;
