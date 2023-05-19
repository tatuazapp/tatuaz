using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Dashboard.Queue.Consumers.Post;

public class GetPostFeedConsumer : TatuazConsumerBase<GetPostFeed, PagedData<BriefPostDto>>
{
    private readonly IUserContext _userContext;
    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Post.Post,
        HistPost,
        Guid
    > _postRepository;
    private readonly IGenericRepository<PostLike, HistPostLike, Guid> _postLikeRepository;
    private readonly DbContext _dbContext;

    public GetPostFeedConsumer(
        ILogger<GetPostFeedConsumer> logger,
        IUserContext userContext,
        IGenericRepository<Shared.Domain.Entities.Models.Post.Post, HistPost, Guid> postRepository,
        IGenericRepository<PostLike, HistPostLike, Guid> postLikeRepository,
        DbContext dbContext
    )
        : base(logger)
    {
        _userContext = userContext;
        _postRepository = postRepository;
        _postLikeRepository = postLikeRepository;
        _dbContext = dbContext;
    }

    protected override async Task<TatuazResult<PagedData<BriefPostDto>>> ConsumeMessage(
        ConsumeContext<GetPostFeed> context
    )
    {
        var withPhotos = context.Message.Photos;
        var withoutPhotos = context.Message.Posts;
        var userSeedInt = _userContext.RequiredCurrentUserEmail().GetHashCode();
        var userSeed = (double)userSeedInt / int.MaxValue;

        var sql =
            @"
SELECT p.""id""
FROM ""post"".""posts"" p
";
        switch (withPhotos)
        {
            case true when !withoutPhotos:
                sql +=
                    @"
INNER JOIN ""post"".""post_photos"" pp ON p.""id"" = pp.""post_id""
";
                break;
            case false when withoutPhotos:
                sql +=
                    @"

LEFT JOIN ""post"".""post_photos"" pp ON p.""id"" = pp.""post_id""
WHERE pp.""post_id"" IS NULL
";
                break;
        }

        sql +=
            @"
ORDER BY CAST(p.""created_at"" AS DATE) DESC, random() DESC
LIMIT (@pageSize)
OFFSET (@pageSize * (@pageNumber - 1))
";

        var parameters = new[]
        {
            new NpgsqlParameter("pageSize", context.Message.PageSize),
            new NpgsqlParameter("pageNumber", context.Message.PageNumber),
        };

        await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

        await _dbContext.Database
            .ExecuteSqlRawAsync(
                "SELECT setseed(@userSeed)",
                new NpgsqlParameter("userSeed", userSeed)
            )
            .ConfigureAwait(false);

        var postIds = await _dbContext
            .Set<Shared.Domain.Entities.Models.Post.Post>()
            .FromSqlRaw(sql, parameters)
            .Select(post => post.Id)
            .ToListAsync(context.CancellationToken)
            .ConfigureAwait(false);

        var postsCount = await _dbContext
            .Set<Shared.Domain.Entities.Models.Post.Post>()
            .CountAsync(context.CancellationToken)
            .ConfigureAwait(false);

        await _dbContext.Database.CommitTransactionAsync().ConfigureAwait(false);

        var spec = new FullSpecification<Shared.Domain.Entities.Models.Post.Post>();
        spec.AddFilter(post => postIds.Contains(post.Id));

        var results = await _postRepository
            .GetBySpecificationWithPagingAsync<BriefPostDto>(
                spec,
                new PagedParams(context.Message.PageNumber, context.Message.PageSize)
            )
            .ConfigureAwait(false);

        var resultsHashSet = results.Data.ToHashSet();
        results.Data = resultsHashSet.OrderBy(x => postIds.IndexOf(x.Id)).ToList();

        var likesSpec = new FullSpecification<PostLike>();
        var resultsIds = results.Data.Select(x => x.Id).ToArray();
        likesSpec.AddFilter(
            x =>
                resultsIds.Contains(x.PostId) && x.UserId == _userContext.RequiredCurrentUserEmail()
        );

        var likes = (
            await _postLikeRepository.GetBySpecificationAsync(likesSpec).ConfigureAwait(false)
        ).ToList();

        foreach (var result in results.Data)
        {
            result.IsLikedByCurrentUser = likes.Any(x => x.PostId == result.Id);
        }

        results.TotalCount = postsCount;
        results.TotalPages = (int)Math.Ceiling((double)postsCount / context.Message.PageSize);

        return CommonResultFactory.Ok(results);
    }
}
