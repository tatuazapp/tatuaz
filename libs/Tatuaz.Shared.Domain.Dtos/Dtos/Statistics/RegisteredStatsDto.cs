using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;

public record RegisteredStatsDto(int Artists, int Clients, int Users) : IDto;
