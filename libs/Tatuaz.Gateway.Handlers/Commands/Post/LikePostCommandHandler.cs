using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Dashboard.Queue.Producers.Post;
using Tatuaz.Gateway.Requests.Commands.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Post;

public class LikePostCommandHandler : IRequestHandler<LikePostCommand, TatuazResult<EmptyDto>>
{
    private readonly IValidator<LikePostDto> _validator;
    private readonly LikePostProducer _producer;

    public LikePostCommandHandler(IValidator<LikePostDto> validator, LikePostProducer producer)
    {
        _validator = validator;
        _producer = producer;
    }

    public async Task<TatuazResult<EmptyDto>> Handle(
        LikePostCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.LikePostDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        return await _producer
            .Send(
                new LikePost(request.LikePostDto.PostId!.Value, request.LikePostDto.Like!.Value),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
