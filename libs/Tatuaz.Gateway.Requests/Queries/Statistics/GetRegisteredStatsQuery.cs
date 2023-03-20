using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Statistics;

public record GetRegisteredStatsQuery : IRequest<TatuazResult<RegisteredStatsDto>>;
