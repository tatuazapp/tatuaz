using System.Collections.Generic;
using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Landing;

public record ListStatsQuery
(
    ListStatsDto ListStatsDto
) : IRequest<TatuazResult<IEnumerable<StatDto>>>;