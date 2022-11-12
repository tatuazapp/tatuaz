using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tatuaz.Gateway.Queue.Contracts.Landing.ListArtistStats;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListArtistStats;
using Tatuaz.Shared.Domain.Dtos.Fakers.Landing.ListArtistStats;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Landing.Queue.Consumers;

public class ListArtistStatsConsumer
    : TatuazConsumerBase<ListArtistStatsOrder, IEnumerable<ArtistStatDto>>
{
    public ListArtistStatsConsumer(ILogger<ListArtistStatsConsumer> logger) : base(logger) { }

    protected override Task<TatuazResult<IEnumerable<ArtistStatDto>>> ConsumeMessage(
        ListArtistStatsOrder message
    )
    {
        var faker = new ArtistStatDtoFaker();
        var result = faker.Generate(message.Amount);
        result = result
            .Select(
                x =>
                    x with
                    {
                        BackgroundUrl =
                            "https://media.vanityfair.com/photos/62d6dfe36b77832a6a80ae46/9:16/w_765,h_1360,c_limit/ryan-gosling-the-gray-man.jpg"
                    }
            )
            .ToList();

        return Task.FromResult(CommonResultFactory.Ok<IEnumerable<ArtistStatDto>>(result));
    }
}
