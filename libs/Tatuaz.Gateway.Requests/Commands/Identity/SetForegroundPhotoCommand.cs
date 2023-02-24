using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Commands.Identity;

public record SetForegroundPhotoCommand(SetForegroundPhotoDto SetForegroundPhotoDto) : IRequest<TatuazResult<Unit>>;
