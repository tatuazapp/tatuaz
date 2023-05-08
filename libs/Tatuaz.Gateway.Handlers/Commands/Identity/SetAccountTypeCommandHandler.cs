using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Consumers.Identity;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Dashboard.Queue.Producers.Identity;
using Tatuaz.Gateway.Requests.Commands.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Identity;

public class SetAccountTypeCommandHandler
    : IRequestHandler<SetAccountTypeCommand, TatuazResult<EmptyDto>>
{
    private readonly IValidator<SetAccountTypeDto> _validator;
    private readonly SetAccountTypeProducer _producer;

    public SetAccountTypeCommandHandler(
        IValidator<SetAccountTypeDto> validator,
        SetAccountTypeProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<EmptyDto>> Handle(
        SetAccountTypeCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.SetAccountTypeDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        return await _producer
            .Send(new SetAccountType(request.SetAccountTypeDto.Artist), cancellationToken)
            .ConfigureAwait(false);
    }
}
