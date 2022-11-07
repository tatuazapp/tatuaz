using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using NodaTime;
using Tatuaz.Gateway.Queue.Contracts;
using Tatuaz.Gateway.Queue.Producers;
using Tatuaz.Gateway.Requests.Queries.Landing;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Landing;

public class
    ListSummaryStatsQueryHandler : IRequestHandler<ListSummaryStatsQuery, TatuazResult<IEnumerable<SummaryStatDto>>>
{
    private readonly IClock _clock;
    private readonly ListSummaryStatsProducer _listSummaryStatsProducer;
    private readonly IValidator<ListSummaryStatsDto> _validator;

    public ListSummaryStatsQueryHandler(
        ListSummaryStatsProducer listSummaryStatsProducer,
        IClock clock,
        IValidator<ListSummaryStatsDto> validator
    )
    {
        _listSummaryStatsProducer = listSummaryStatsProducer;
        _clock = clock;
        _validator = validator;
    }

    public async Task<TatuazResult<IEnumerable<SummaryStatDto>>> Handle(ListSummaryStatsQuery request,
        CancellationToken cancellationToken)
    {
        var validationResult =
            await _validator.ValidateAsync(request.ListSummaryStatsDto, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<IEnumerable<SummaryStatDto>>(validationResult);
        }

        var from = request.ListSummaryStatsDto.TimePeriod switch
        {
            SummaryStatTimePeriod.Day => _clock.GetCurrentInstant().Minus(Duration.FromDays(1)),
            SummaryStatTimePeriod.Week => _clock.GetCurrentInstant().Minus(Duration.FromDays(7)),
            SummaryStatTimePeriod.Month => _clock.GetCurrentInstant().Minus(Duration.FromDays(31)),
            _ => throw new ArgumentException(nameof(request.ListSummaryStatsDto.TimePeriod))
        };

        var result = await _listSummaryStatsProducer
            .Send(new ListSummaryStatsOrder(from, _clock.GetCurrentInstant(), request.ListSummaryStatsDto.Count),
                cancellationToken)
            .ConfigureAwait(false);

        if (result == null)
        {
            throw new NullReferenceException();
        }

        return result;
    }
}
