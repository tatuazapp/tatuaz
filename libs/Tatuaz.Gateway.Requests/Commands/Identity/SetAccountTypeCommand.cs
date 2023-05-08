using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Commands.Identity;

public record SetAccountTypeCommand(SetAccountTypeDto SetAccountTypeDto)
    : IRequest<TatuazResult<EmptyDto>>;
