using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Commands.Post;

public record FinalizePostCommand(FinalizePostDto FinalizePostDto)
    : IRequest<TatuazResult<EmptyDto>>;
