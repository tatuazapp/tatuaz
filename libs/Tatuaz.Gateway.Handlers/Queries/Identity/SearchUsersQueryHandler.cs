using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Dashboard.Queue.Producers.Identity;
using Tatuaz.Gateway.Requests.Queries.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Identity;

public class SearchUsersQueryHandler
    : IRequestHandler<SearchUsersQuery, TatuazResult<PagedData<BriefUserDto>>>
{
    private readonly IValidator<SearchUsersDto> _validator;
    private readonly SearchUsersProducer _producer;

    public SearchUsersQueryHandler(
        IValidator<SearchUsersDto> validator,
        SearchUsersProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<PagedData<BriefUserDto>>> Handle(
        SearchUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.SearchUsersDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<PagedData<BriefUserDto>>(validationResult);
        }

        var result = await _producer
            .Send(
                new SearchUsers(
                    request.SearchUsersDto.Query!,
                    request.SearchUsersDto.PageNumber!.Value,
                    request.SearchUsersDto.PageSize!.Value,
                    request.SearchUsersDto.OnlyArtists!.Value
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
        return result;
    }
}
