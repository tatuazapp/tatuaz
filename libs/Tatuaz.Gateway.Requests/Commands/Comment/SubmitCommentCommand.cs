using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Commands.Comment;

public record SubmitCommentCommand(SubmitCommentDto CommentDto)
    : IRequest<TatuazResult<SubmittedCommentDto>>;
