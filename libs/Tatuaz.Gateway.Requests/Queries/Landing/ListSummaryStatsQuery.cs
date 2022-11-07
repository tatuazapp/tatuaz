using System.Collections.Generic;
using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Landing;

public record ListSummaryStatsQuery
(
    ListSummaryStatsDto ListSummaryStatsDto
) : IRequest<TatuazResult<IEnumerable<SummaryStatDto>>>;
