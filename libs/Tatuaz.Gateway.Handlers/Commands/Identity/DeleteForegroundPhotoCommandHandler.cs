using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Dashboard.Queue.Producers.Identity;
using Tatuaz.Gateway.Requests.Commands.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Identity;

public class DeleteForegroundPhotoCommandHandler
    : IRequestHandler<DeleteForegroundPhotoCommand, TatuazResult<EmptyDto>>
{
    private readonly IValidator<DeleteForegroundPhotoDto> _validator;
    private readonly DeleteForegroundPhotoProducer _producer;

    public DeleteForegroundPhotoCommandHandler(
        IValidator<DeleteForegroundPhotoDto> validator,
        DeleteForegroundPhotoProducer producer
    )
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<EmptyDto>> Handle(
        DeleteForegroundPhotoCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.DeleteForegroundPhotoDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        return await _producer
            .Send(new DeleteForegroundPhoto(), cancellationToken)
            .ConfigureAwait(false);
    }
}
