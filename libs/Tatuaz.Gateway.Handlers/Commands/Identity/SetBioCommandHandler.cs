using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Dashboard.Queue.Producers.Identity;
using Tatuaz.Gateway.Requests.Commands.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Identity;

public class SetBioCommandHandler : IRequestHandler<SetBioCommand, TatuazResult<EmptyDto>>
{
    private readonly IValidator<SetBioDto> _validator;
    private readonly SetBioProducer _producer;

    public SetBioCommandHandler(IValidator<SetBioDto> validator, SetBioProducer producer)
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<EmptyDto>> Handle(
        SetBioCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.SetBioDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        return await _producer
            .Send(new SetBio(request.SetBioDto.Bio), cancellationToken)
            .ConfigureAwait(false);
    }
}
