using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Post;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Tatuaz.Dashboard.Queue.Consumers.Post;

public class FinalizePostConsumer : TatuazConsumerBase<FinalizePost, EmptyDto>
{
    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Post.Post,
        HistPost,
        Guid
    > _postRepository;
    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Photo.Photo,
        HistPhoto,
        Guid
    > _photoRepository;
    private readonly IGenericRepository<InitialPost, HistInitialPost, Guid> _initialPostRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public FinalizePostConsumer(
        ILogger<FinalizePostConsumer> logger,
        IGenericRepository<Shared.Domain.Entities.Models.Post.Post, HistPost, Guid> postRepository,
        IGenericRepository<
            Shared.Domain.Entities.Models.Photo.Photo,
            HistPhoto,
            Guid
        > photoRepository,
        IGenericRepository<InitialPost, HistInitialPost, Guid> initialPostRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork
    )
        : base(logger)
    {
        _postRepository = postRepository;
        _photoRepository = photoRepository;
        _initialPostRepository = initialPostRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<TatuazResult<EmptyDto>> ConsumeMessage(
        ConsumeContext<FinalizePost> context
    )
    {
        var initialPostSpec = new FullSpecification<InitialPost>();
        initialPostSpec.AddFilter(x => x.Id == context.Message.InitialPostId);
        initialPostSpec.UseInclude(x => x.Include(y => y.InitialPostPhotos));
        var initialPost = (
            await _initialPostRepository
                .GetBySpecificationAsync(initialPostSpec, context.CancellationToken)
                .ConfigureAwait(false)
        ).FirstOrDefault();

        if (initialPost == null)
        {
            return FinalizePostResultFactory.InitialPostDoesNotExist<EmptyDto>();
        }

        var validationResult = new ValidationResult();

        if (initialPost.CreatedBy != _userContext.RequiredCurrentUserEmail())
        {
            return FinalizePostResultFactory.UserIsNotTheAuthorOfTheInitialPost<EmptyDto>();
        }

        foreach (var photo in context.Message.Photo)
        {
            if (!initialPost.InitialPostPhotos.Select(x => x.PhotoId).Contains(photo.PhotoId))
            {
                validationResult.Errors.Add(
                    new ValidationFailure(nameof(photo.PhotoId), "Photo not found on initial post")
                    {
                        ErrorCode = FinalizePostErrorCodes.PhotoNotFoundOnInitialPost
                    }
                );
            }
        }

        foreach (var initialPhoto in initialPost.InitialPostPhotos)
        {
            if (!context.Message.Photo.Select(x => x.PhotoId).Contains(initialPhoto.PhotoId))
            {
                validationResult.Errors.Add(
                    new ValidationFailure(
                        nameof(initialPhoto.PhotoId),
                        $"Photo {initialPhoto.PhotoId} is missing"
                    )
                    {
                        ErrorCode = FinalizePostErrorCodes.PhotoMissing
                    }
                );
            }
        }

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<EmptyDto>(validationResult);
        }

        var post = new Shared.Domain.Entities.Models.Post.Post
        {
            AuthorId = _userContext.RequiredCurrentUserEmail(),
            Description = context.Message.Description,
            Photos = context.Message.Photo
                .Select(x => new PostPhoto { PhotoId = x.PhotoId })
                .ToList(),
        };
        _postRepository.Create(post);

        foreach (var photo in context.Message.Photo)
        {
            var photoToEdit = await _photoRepository
                .GetByIdAsync(photo.PhotoId, true, cancellationToken: context.CancellationToken)
                .ConfigureAwait(false);
            if (photoToEdit == null)
            {
                return CommonResultFactory.InternalError<EmptyDto>();
            }

            var categories = photo.CategoryIds
                .Select(x => new PhotoCategory { CategoryId = x })
                .ToList();
            photoToEdit.PhotoCategories = categories;
        }

        _initialPostRepository.Delete(initialPost);

        await _unitOfWork.SaveChangesAsync(context.CancellationToken).ConfigureAwait(false);

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
