using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MassTransit;
using MediatR;
using Tatuaz.Dashboard.Queue.Consumers.Photo;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Dashboard.Queue.Producers.Photo;
using Tatuaz.Dashboard.Queue.Producers.Post;
using Tatuaz.Gateway.Requests.Commands.Post;
using Tatuaz.Scheduler.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Helpers.DataStructures;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Services;

namespace Tatuaz.Gateway.Handlers.Commands.Post;

public class UploadPostPhotosCommandHandler
    : IRequestHandler<UploadPostPhotosCommand, TatuazResult<UploadedPhotosDto>>
{
    private readonly IValidator<UploadPostPhotosDto> _validator;
    private readonly IPhotoService _photoService;
    private readonly UploadPostPhotosProducer _uploadPostPhotosProducer;

    public UploadPostPhotosCommandHandler(
        IValidator<UploadPostPhotosDto> validator,
        IPhotoService photoService,
        UploadPostPhotosProducer uploadPostPhotosProducer
    )
    {
        _validator = validator;
        _photoService = photoService;
        _uploadPostPhotosProducer = uploadPostPhotosProducer;
    }

    public async Task<TatuazResult<UploadedPhotosDto>> Handle(
        UploadPostPhotosCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.UploadPostPhotosDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<UploadedPhotosDto>(validationResult);
        }

        using var streams = new DisposableList<MemoryStream>();
        for (var i = 0; i < request.UploadPostPhotosDto.Photos.Length; i++)
        {
            var stream = new MemoryStream();
            await request.UploadPostPhotosDto.Photos[i]
                .CopyToAsync(stream, cancellationToken)
                .ConfigureAwait(false);
            if (!_photoService.ValidatePhotoHeaders(stream))
            {
                var validationError = new ValidationFailure(
                    nameof(request.UploadPostPhotosDto.Photos),
                    "Invalid file format for photo number " + (i + 1) + "."
                )
                {
                    ErrorCode = UploadPostPhotosErrorCodes.InvalidFileFormat
                };
                validationResult.Errors.Add(validationError);
                return CommonResultFactory.ValidationError<UploadedPhotosDto>(validationResult);
            }

            streams.Add(stream);
        }

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<UploadedPhotosDto>(validationResult);
        }

        return await _uploadPostPhotosProducer
            .Send(
                new UploadPostPhotos(streams.Select(x => x.ToArray()).ToArray()),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
