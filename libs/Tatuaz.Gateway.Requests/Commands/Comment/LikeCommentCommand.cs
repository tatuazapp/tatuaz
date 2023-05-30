using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Commands.Comment;

public record LikeCommentCommand(LikeCommentDto LikeCommentDto) : IRequest<TatuazResult<EmptyDto>>;
