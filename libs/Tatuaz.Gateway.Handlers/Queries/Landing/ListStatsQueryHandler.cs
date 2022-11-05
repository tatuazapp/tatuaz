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
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing.Enums;
using Tatuaz.Shared.Domain.Dtos.Validators.Landing;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Landing;

public class ListStatsQueryHandler : IRequestHandler<ListStatsQuery, TatuazResult<IEnumerable<StatDto>>>
{
    private readonly IClock _clock;
    private readonly IUserAccessor _userAccessor;
    private readonly ILogger<ListStatsProducer> _logger;
    private readonly IRequestClient<ListStatsOrder> _requestClient;

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

    public async Task<TatuazResult<IEnumerable<StatDto>>> Handle(ListStatsQuery request,
        CancellationToken cancellationToken)
    {
        var validator = new GetStatsDtoValidator();
        var validationResult =
            await validator.ValidateAsync(request.ListStatsDto, cancellationToken).ConfigureAwait(false);
        if (validationResult.IsValid == false)
        {
            return CommonResultFactory.ValidationError<IEnumerable<StatDto>>(validationResult);
        }

        var producer = new ListStatsProducer(_requestClient, _userAccessor, _logger);
        var from = request.ListStatsDto.TimePeriod switch
        {
            StatsTimePeriod.Day => _clock.GetCurrentInstant().Minus(Duration.FromDays(1)),
            StatsTimePeriod.Week => _clock.GetCurrentInstant().Minus(Duration.FromDays(7)),
            StatsTimePeriod.Month => _clock.GetCurrentInstant().Minus(Duration.FromDays(31)),
            _ => throw new ArgumentException(nameof(request.ListStatsDto.TimePeriod))
        };

        var result = await producer
            .Send(new ListStatsOrder(from, _clock.GetCurrentInstant(), request.ListStatsDto.Count), cancellationToken)
            .ConfigureAwait(false);

        if (result == null)
        {
            throw new NullReferenceException();
        }

        return result;
    }
}
