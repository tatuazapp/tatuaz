using System.Collections.Generic;
using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Landing;

public record ListArtistStatsQuery
(
    ListArtistStatsDto ListArtistStatDto
) : IRequest<TatuazResult<IEnumerable<ArtistStatDto>>>;
