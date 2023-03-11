using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Statistics;
using Tatuaz.Shared.Domain.Dtos.Dtos.Statistics;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Statistics;

public class GetRegisteredStatsConsumer : TatuazConsumerBase<GetRegisteredStats, RegisteredStatsDto>
{
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;

    public GetRegisteredStatsConsumer(
        ILogger<GetRegisteredStatsConsumer> logger,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository)
        : base(logger)
    {
        _userRepository = userRepository;
    }

    protected override async Task<TatuazResult<RegisteredStatsDto>> ConsumeMessage(
        ConsumeContext<GetRegisteredStats> context)
    {
        var users = (int) await _userRepository.CountByPredicateAsync(x => true).ConfigureAwait(false);

        //TODO: get artists and clients counts once they are implemented
        var result = new RegisteredStatsDto(users/3, users/2, users);

        return CommonResultFactory.Ok<RegisteredStatsDto>(result);
    }
}
