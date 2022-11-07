using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using NodaTime;
using Tatuaz.Gateway.Queue.Contracts;
using Tatuaz.Gateway.Queue.Producers;
using Tatuaz.Gateway.Requests.Queries.Landing;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.ListSummaryStats;
using Tatuaz.Shared.Domain.Dtos.Validators.Landing.ListSummaryStats;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Landing;

public class ListStatsQueryHandler : IRequestHandler<ListSummaryStatsQuery, TatuazResult<IEnumerable<SummaryStatDto>>>
{
    private readonly IClock _clock;
    private readonly ILogger<ListStatsProducer> _logger;
    private readonly IRequestClient<ListStatsOrder> _requestClient;
    private readonly IUserAccessor _userAccessor;

    public ListStatsQueryHandler(
        IRequestClient<ListStatsOrder> requestClient,
        IClock clock,
        IUserAccessor userAccessor,
        ILogger<ListStatsProducer> logger)
    {
        _requestClient = requestClient;
        _clock = clock;
        _userAccessor = userAccessor;
        _logger = logger;
    }

    public async Task<TatuazResult<IEnumerable<SummaryStatDto>>> Handle(ListSummaryStatsQuery request,
        CancellationToken cancellationToken)
    {
        var validator = new ListSummaryStatsDtoValidator();
        var validationResult =
            await validator.ValidateAsync(request.ListSummaryStatsDto, cancellationToken).ConfigureAwait(false);
        if (validationResult.IsValid == false)
        {
            return CommonResultFactory.ValidationError<IEnumerable<SummaryStatDto>>(validationResult);
        }

        var producer = new ListStatsProducer(_requestClient, _userAccessor, _logger);
        var from = request.ListSummaryStatsDto.TimePeriod switch
        {
            SummaryStatTimePeriod.Day => _clock.GetCurrentInstant().Minus(Duration.FromDays(1)),
            SummaryStatTimePeriod.Week => _clock.GetCurrentInstant().Minus(Duration.FromDays(7)),
            SummaryStatTimePeriod.Month => _clock.GetCurrentInstant().Minus(Duration.FromDays(31)),
            _ => throw new ArgumentException(nameof(request.ListSummaryStatsDto.TimePeriod))
        };

        var result = await producer
            .Send(new ListStatsOrder(from, _clock.GetCurrentInstant(), request.ListSummaryStatsDto.Count), cancellationToken)
            .ConfigureAwait(false);

        if (result == null)
        {
            throw new NullReferenceException();
        }

        return result;
    }
}
