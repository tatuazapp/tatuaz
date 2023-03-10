using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using FluentValidation.Results;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Tatuaz.Dashboard.Queue.Consumers.Photo;

public class FinalizePostConsumer : TatuazConsumerBase<FinalizePost, EmptyDto>
{
    private readonly IGenericRepository<Post, HistPost, Guid> _postRepository;
    private readonly IGenericRepository<Shared.Domain.Entities.Models.Photo.Photo, HistPhoto, Guid> _photoRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public FinalizePostConsumer(
        ILogger<FinalizePostConsumer> logger,
        IGenericRepository<Post, HistPost, Guid> postRepository,
        IGenericRepository<Shared.Domain.Entities.Models.Photo.Photo, HistPhoto, Guid> photoRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork
            ) : base(logger)
    {
        _postRepository = postRepository;
        _photoRepository = photoRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<TatuazResult<EmptyDto>> ConsumeMessage(ConsumeContext<FinalizePost> context)
    {
        var validationResult = new ValidationResult();

        foreach (var photo in context.Message.Photo)
        {
            if(!await _photoRepository.ExistsByIdAsync(photo.PhotoId, context.CancellationToken).ConfigureAwait(false))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(photo.PhotoId), $"Photo with id {photo.PhotoId} does not exist")
                {
                    ErrorCode = FinalizePostErrorCodes.PhotoDoesNotExist
                });
            }
        }

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }


        var post = new Post
        {
            AuthorId = _userContext.RequiredCurrentUserEmail(),
            Description = context.Message.Description,
            Photos = context.Message.Photo.Select(x => new PostPhoto { PhotoId = x.PhotoId }).ToList(),
        };
        _postRepository.Create(post);

        foreach (var photo in context.Message.Photo)
        {
            var photoToEdit = await _photoRepository.GetByIdAsync(photo.PhotoId, true, cancellationToken: context.CancellationToken).ConfigureAwait(false);
            if(photoToEdit == null)
            {
                return CommonResultFactory.InternalError<EmptyDto>();
            }
            var categories = photo.CategoryIds.Select(x => new PhotoCategory { CategoryId = x }).ToList();
            photoToEdit.PhotoCategories = categories;
        }
        await _unitOfWork.SaveChangesAsync(context.CancellationToken).ConfigureAwait(false);

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
