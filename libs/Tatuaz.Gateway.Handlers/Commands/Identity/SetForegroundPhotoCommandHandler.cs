using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Dashboard.Queue.Producers.Identity;
using Tatuaz.Gateway.Requests.Commands.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Identity;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Services;

namespace Tatuaz.Gateway.Handlers.Commands.Identity;

public class SetForegroundPhotoCommandHandler
    : IRequestHandler<SetForegroundPhotoCommand, TatuazResult<EmptyDto>>
{
    private readonly IPhotoService _photoService;
    private readonly SetForegroundPhotoProducer _producer;
    private readonly IValidator<SetForegroundPhotoDto> _validator;

    public SetForegroundPhotoCommandHandler(
        IPhotoService photoService,
        SetForegroundPhotoProducer producer,
        IValidator<SetForegroundPhotoDto> validator
    )
    {
        _photoService = photoService;
        _producer = producer;
        _validator = validator;
    }

    public async Task<TatuazResult<EmptyDto>> Handle(
        SetForegroundPhotoCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.SetForegroundPhotoDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        using var stream = new MemoryStream();
        await request.SetForegroundPhotoDto.Photo!
            .CopyToAsync(stream, cancellationToken)
            .ConfigureAwait(false);
        if (!_photoService.ValidatePhotoHeaders(stream))
            return SetBackgroundPhotoResultFactory.InvalidFileFormat<EmptyDto>();

        var photoBytes = stream.ToArray();

        return await _producer
            .Send(new SetForegroundPhoto(photoBytes), cancellationToken)
            .ConfigureAwait(false);
    }
}
