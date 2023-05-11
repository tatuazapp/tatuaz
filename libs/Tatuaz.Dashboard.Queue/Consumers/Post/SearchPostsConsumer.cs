using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Tatuaz.Dashboard.Queue.Contracts.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Post;

public class SearchPostsConsumer : TatuazConsumerBase<SearchPosts, PagedData<BriefPostDto>>
{
    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Post.Post,
        HistPost,
        Guid
    > _postRepository;
    private readonly DbContext _dbContext;

    public SearchPostsConsumer(
        ILogger<SearchPostsConsumer> logger,
        IGenericRepository<Shared.Domain.Entities.Models.Post.Post, HistPost, Guid> postRepository,
        DbContext dbContext
    )
        : base(logger)
    {
        _postRepository = postRepository;
        _dbContext = dbContext;
    }

    protected override async Task<TatuazResult<PagedData<BriefPostDto>>> ConsumeMessage(
        ConsumeContext<SearchPosts> context
    )
    {
        var queryTokens = context.Message.Query
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(token => token.Trim())
            .Where(token => !string.IsNullOrEmpty(token))
            .ToArray();

        var postIds = new List<Guid>();

        if (queryTokens.Any())
        {
            var sqlQuery = string.Join(
                " || ' & ' || ",
                queryTokens.Select((_, index) => $"@token{index}")
            );

            var rawSql =
                $@"SELECT p.""id""
                   FROM ""post"".""posts"" p
                   JOIN ""identity"".""tatuaz_users"" u ON p.""author_id"" = u.""id""
                   WHERE to_tsvector('english', p.""description"") @@ to_tsquery('english', {sqlQuery})
                   OR to_tsvector('english', u.""username"") @@ to_tsquery('english', {sqlQuery})";

            var sqlQueryParams = queryTokens
                .Select((token, index) => new NpgsqlParameter($"token{index}", $"*{token}*"))
                .ToArray();

            postIds = await _dbContext
                .Set<Shared.Domain.Entities.Models.Post.Post>()
                .FromSqlRaw(rawSql, sqlQueryParams)
                .Select(x => x.Id)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        var spec = new FullSpecification<Shared.Domain.Entities.Models.Post.Post>();

        if (queryTokens.Any())
        {
            spec.AddFilter(x => postIds.Contains(x.Id));
        }

        if (!context.Message.Posts)
        {
            spec.AddFilter(x => x.Photos.Count > 0);
        }

        if (!context.Message.Photos)
        {
            spec.AddFilter(x => x.Photos.Count == 0);
        }

        spec.AddOrder(x => x.CreatedAt, OrderDirection.Descending);

        var results = await _postRepository
            .GetBySpecificationWithPagingAsync<BriefPostDto>(
                spec,
                new PagedParams(context.Message.PageNumber, context.Message.PageSize)
            )
            .ConfigureAwait(false);

        return CommonResultFactory.Ok(results);
    }
}
