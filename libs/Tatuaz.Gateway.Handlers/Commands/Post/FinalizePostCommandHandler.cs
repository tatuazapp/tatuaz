using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Dashboard.Queue.Producers.Photo;
using Tatuaz.Dashboard.Queue.Producers.Post;
using Tatuaz.Gateway.Requests.Commands.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Post;

public class FinalizePostCommandHandler : IRequestHandler<FinalizePostCommand, TatuazResult<EmptyDto>>
{
    private readonly IValidator<FinalizePostDto> _validator;
    private readonly FinalizePostProducer _finalizePostProducer;

    public FinalizePostCommandHandler(
        IValidator<FinalizePostDto> validator,
        FinalizePostProducer finalizePostProducer
    )
    {
        _validator = validator;
        _finalizePostProducer = finalizePostProducer;
    }

    public async Task<TatuazResult<EmptyDto>> Handle(FinalizePostCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.FinalizePostDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        var result = await _finalizePostProducer
            .Send(new FinalizePost(request.FinalizePostDto.InitialPostId, request.FinalizePostDto.Description, request.FinalizePostDto.PhotoInfoDtos),
                cancellationToken)
            .ConfigureAwait(false);

        return result;
    }
}
