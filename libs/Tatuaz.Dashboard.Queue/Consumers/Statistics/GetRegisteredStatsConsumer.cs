using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Statistics;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Statistics;

public class GetRegisteredStatsConsumer : TatuazConsumerBase<GetRegisteredStats, RegisteredStatsDto>
{
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;

    public GetRegisteredStatsConsumer(
        ILogger<GetRegisteredStatsConsumer> logger,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository
    )
        : base(logger)
    {
        _userRepository = userRepository;
    }

    protected override async Task<TatuazResult<RegisteredStatsDto>> ConsumeMessage(
        ConsumeContext<GetRegisteredStats> context
    )
    {
        var users = (int)
            await _userRepository.CountByPredicateAsync(x => true).ConfigureAwait(false);

        var artists = (int)
            await _userRepository
                .CountByPredicateAsync(x => x.UserRoles.Any(y => y.Role.Id == TatuazRole.ArtistId))
                .ConfigureAwait(false);

        var clients = (int)
            await _userRepository
                .CountByPredicateAsync(x => x.BookingRequests.Any())
                .ConfigureAwait(false);

        var result = new RegisteredStatsDto(artists, clients, users);

        return CommonResultFactory.Ok(result);
    }
}
