using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Dashboard.Queue.Producers.Photo;
using Tatuaz.Scheduler.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Post;

public class UploadPostPhotosConsumer : TatuazConsumerBase<UploadPostPhotos, UploadedPhotosDto>
{
    private readonly AddPhotoProducer _addPhotoProducer;
    private readonly IGenericRepository<InitialPost, HistInitialPost, Guid> _initialPostRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadPostPhotosConsumer(
        ILogger<UploadPostPhotosConsumer> logger,
        AddPhotoProducer addPhotoProducer,
        IGenericRepository<InitialPost, HistInitialPost, Guid> initialPostRepository,
        IUnitOfWork unitOfWork
    ) : base(logger)
    {
        _addPhotoProducer = addPhotoProducer;
        _initialPostRepository = initialPostRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<TatuazResult<UploadedPhotosDto>> ConsumeMessage(
        ConsumeContext<UploadPostPhotos> context)
    {
        var photoIds = new Guid[context.Message.Photos.Length];

        for (var i = 0; i < context.Message.Photos.Length; i++)
        {
            var addPhotoResponse = await _addPhotoProducer
                .Send(new AddPhoto(context.Message.Photos[i]), context.CancellationToken).ConfigureAwait(false);
            if (!addPhotoResponse.Successful)
            {
                return CommonResultFactory.InternalError<UploadedPhotosDto>("Failed to add photo.");
            }

            photoIds[i] = addPhotoResponse.Value;
        }

        var initialPost = new InitialPost
        {
            InitialPostPhotos = photoIds
                .Select(x => new InitialPostPhoto { PhotoId = x })
                .ToList()
        };

        _initialPostRepository.Create(initialPost);
        await _unitOfWork.SaveChangesAsync(context.CancellationToken).ConfigureAwait(false);

        await context.Publish(new SchedulePostIntegrityCheck(initialPost.Id), context.CancellationToken)
            .ConfigureAwait(false);

        var uploadedPhotosDto = new UploadedPhotosDto(initialPost.Id, photoIds);

        return CommonResultFactory.Ok(uploadedPhotosDto);
    }
}
