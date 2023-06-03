using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Comment;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Dtos.Validators.Comment;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Tatuaz.Dashboard.Queue.Consumers.Comment;

public class SubmitCommentConsumer : TatuazConsumerBase<SubmitComment, SubmittedCommentDto>
{
    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Post.Comment,
        HistComment,
        Guid
    > _commentRepository;

    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public SubmitCommentConsumer(
        ILogger<SubmitCommentConsumer> logger,
        IGenericRepository<
            Shared.Domain.Entities.Models.Post.Comment,
            HistComment,
            Guid
        > commentRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork
    )
        : base(logger)
    {
        _commentRepository = commentRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<TatuazResult<SubmittedCommentDto>> ConsumeMessage(
        ConsumeContext<SubmitComment> context
    )
    {
        var comment = new Shared.Domain.Entities.Models.Post.Comment()
        {
            ParentCommentId = context.Message.ParentCommentId,
            PostId = context.Message.PostId,
            UserId = _userContext.RequiredCurrentUserEmail(),
            Content = context.Message.Content
        };

        _commentRepository.Create(comment);

        await _unitOfWork.SaveChangesAsync(context.CancellationToken).ConfigureAwait(false);

        return CommonResultFactory.Ok(new SubmittedCommentDto(comment.Id, comment.Content));
    }
}
