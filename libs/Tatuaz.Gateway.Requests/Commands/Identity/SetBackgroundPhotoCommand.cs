using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Commands.Identity;

public record SetBackgroundPhotoCommand(SetBackgroundPhotoDto SetBackgroundPhotoDto)
    : IRequest<TatuazResult<EmptyDto>>;
