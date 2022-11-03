using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tatuaz.Gateway.Requests.Queries.Landing;
using Tatuaz.Shared.Domain.Dtos.Dtos.Landing;
using Tatuaz.Shared.Domain.Dtos.Validators.Landing;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Landing;

public class ListStatsQueryHandler : IRequestHandler<ListStatsQuery, TatuazResult<IEnumerable<StatDto>>>
{
    public async Task<TatuazResult<IEnumerable<StatDto>>> Handle(ListStatsQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetStatsDtoValidator();
        var validationResult = await validator.ValidateAsync(request.ListStatsDto, cancellationToken).ConfigureAwait(false);
        if(validationResult.IsValid == false)
        {
            return CommonResultFactory.ValidationError<IEnumerable<StatDto>>(validationResult);
        }

        return CommonResultFactory.Ok<IEnumerable<StatDto>>(Array.Empty<StatDto>());
    }
}