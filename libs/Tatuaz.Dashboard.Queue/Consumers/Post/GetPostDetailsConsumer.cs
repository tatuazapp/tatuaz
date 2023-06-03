using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post.GetPostDetails;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Post;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Dashboard.Queue.Consumers.Post;

public class GetPostDetailsConsumer : TatuazConsumerBase<GetPostDetails, PostDetailsDto>
{
    private readonly IUserContext _userContext;

    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Post.Post,
        HistPost,
        Guid
    > _postRepository;

    private readonly IGenericRepository<PostPhoto, HistPostPhoto, Guid> _postPhotoRepository;

    public GetPostDetailsConsumer(
        ILogger<GetPostDetailsConsumer> logger,
        IUserContext userContext,
        IGenericRepository<Shared.Domain.Entities.Models.Post.Post, HistPost, Guid> postRepository,
        DbContext dbContext,
        IGenericRepository<PostPhoto, HistPostPhoto, Guid> postPhotoRepository
    )
        : base(logger)
    {
        _userContext = userContext;
        _postRepository = postRepository;
        _postPhotoRepository = postPhotoRepository;
    }

    protected override async Task<TatuazResult<PostDetailsDto>> ConsumeMessage(
        ConsumeContext<GetPostDetails> context
    )
    {
        var postSpec = new FullSpecification<Shared.Domain.Entities.Models.Post.Post>();
        postSpec
            .AddFilter(x => x.Id == context.Message.PostId)
            .UseInclude(x => x.Include(y => y.Likes))
            .UseInclude(x => x.Include(y => y.Comments).ThenInclude(y => y.Likes))
            .UseInclude(x => x.Include(y => y.Comments));

        var details = (
            await _postRepository
                .GetBySpecificationAsync<PostDetailsDto>(postSpec, context.CancellationToken)
                .ConfigureAwait(false)
        ).FirstOrDefault();

        if (details == null)
        {
            return GetPostDetailsResultFactory.PostDoesNotExist<PostDetailsDto>();
        }

        var photoSpec = new FullSpecification<PostPhoto>();
        photoSpec.AddFilter(x => x.PostId == details.Id).UseInclude(x => x.Include(y => y.Photo));
        var postPhotos = (
            await _postPhotoRepository
                .GetBySpecificationAsync<PostPhotoDto>(photoSpec, context.CancellationToken)
                .ConfigureAwait(false)
        ).ToList();

        details.Photos = postPhotos;
        return CommonResultFactory.Ok(details);
    }
}
