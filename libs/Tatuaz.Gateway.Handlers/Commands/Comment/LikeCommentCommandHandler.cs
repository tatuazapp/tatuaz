using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Comment;
using Tatuaz.Dashboard.Queue.Producers.Comment;
using Tatuaz.Gateway.Requests.Commands.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Comment;

public class LikeCommentCommandHandler : IRequestHandler<LikeCommentCommand, TatuazResult<EmptyDto>>
{
    private readonly IValidator<LikeCommentDto> _validator;
    private readonly LikeCommentProducer _producer;

    public LikeCommentCommandHandler(LikeCommentProducer producer, IValidator<LikeCommentDto> validator)
    {
        _producer = producer;
        _validator = validator;
    }

    public async Task<TatuazResult<EmptyDto>> Handle(LikeCommentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(request.LikeCommentDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        return await _producer
            .Send(new LikeComment(request.LikeCommentDto.CommentId!.Value,
                    request.LikeCommentDto.Like!.Value),
                cancellationToken).ConfigureAwait(false);
    }
}
