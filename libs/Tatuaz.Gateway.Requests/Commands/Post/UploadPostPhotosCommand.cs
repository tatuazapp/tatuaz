using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Commands.Post;

public record UploadPostPhotosCommand(UploadPostPhotosDto UploadPostPhotosDto)
    : IRequest<TatuazResult<UploadedPhotosDto>>;
