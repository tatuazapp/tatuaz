using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Dashboard.Queue.Producers.Identity;
using Tatuaz.Gateway.Requests.Queries.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Identity;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, TatuazResult<UserDto>>
{
    private readonly IValidator<GetUserDto> _validator;
    private readonly GetUserProducer _producer;

    public GetUserQueryHandler(IValidator<GetUserDto> validator, GetUserProducer producer)
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<UserDto>> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.GetUserDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<UserDto>(validationResult);
        }

        var result = await _producer
            .Send(new GetUser(request.GetUserDto.Username!), cancellationToken)
            .ConfigureAwait(false);
        return result;
    }
}
