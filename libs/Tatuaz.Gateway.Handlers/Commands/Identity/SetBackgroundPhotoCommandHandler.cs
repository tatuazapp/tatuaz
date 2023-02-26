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

public class SetBackgroundPhotoCommandHandler
    : IRequestHandler<SetBackgroundPhotoCommand, TatuazResult<EmptyDto>>
{
    private readonly IPhotoService _photoService;
    private readonly SetBackgroundPhotoProducer _producer;
    private readonly IValidator<SetBackgroundPhotoDto> _validator;

    public SetBackgroundPhotoCommandHandler(
        IPhotoService photoService,
        SetBackgroundPhotoProducer producer,
        IValidator<SetBackgroundPhotoDto> validator
    )
    {
        _photoService = photoService;
        _producer = producer;
        _validator = validator;
    }

    public async Task<TatuazResult<EmptyDto>> Handle(
        SetBackgroundPhotoCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.SetBackgroundPhotoDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        using var stream = new MemoryStream();
        await request.SetBackgroundPhotoDto.Photo!
            .CopyToAsync(stream, cancellationToken)
            .ConfigureAwait(false);
        if (!_photoService.ValidatePhotoHeaders(stream))
            return SetBackgroundPhotoResultFactory.InvalidFileFormat<EmptyDto>();

        var photoBytes = stream.ToArray();

        return await _producer
            .Send(new SetBackgroundPhoto(photoBytes), cancellationToken)
            .ConfigureAwait(false);
    }
}
