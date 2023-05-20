using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Emails;
using Tatuaz.Dashboard.Queue.Contracts.Comment;
using Tatuaz.Dashboard.Queue.Producers.Comment;
using Tatuaz.Gateway.Requests.Commands.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Comment;

public class SubmitCommentCommandHandler
    : IRequestHandler<SubmitCommentCommand, TatuazResult<SubmittedCommentDto>>
{
    private readonly IValidator<SubmitCommentDto> _validator;
    private readonly SubmitCommentProducer _submitCommentProducer;

    public SubmitCommentCommandHandler(
        IValidator<SubmitCommentDto> validator,
        SubmitCommentProducer submitCommentProducer
    )
    {
        _validator = validator;
        _submitCommentProducer = submitCommentProducer;
    }

    public async Task<TatuazResult<SubmittedCommentDto>> Handle(
        SubmitCommentCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.CommentDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<SubmittedCommentDto>(validationResult);
        }

        var result = await _submitCommentProducer
            .Send(
                new SubmitComment(
                    request.CommentDto.PostId,
                    request.CommentDto.ParentCommentId,
                    request.CommentDto.Content
                ),
                cancellationToken
            )
            .ConfigureAwait(false);

        return result;
    }
}
