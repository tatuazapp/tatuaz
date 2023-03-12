using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Dashboard.Queue.Producers.Photo;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Post;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;

namespace Tatuaz.TatuazSchedulerJobs.Post;

public class PostIntegrityCheckJob : IJob
{
    private readonly ILogger<PostIntegrityCheckJob> _logger;
    private readonly IGenericRepository<InitialPost, HistInitialPost, Guid> _initialPostRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeletePhotoProducer _deletePhotoProducer;
    public static readonly JobKey Key = new("PostIntegrityCheck", "Post");

    public PostIntegrityCheckJob(
        ILogger<PostIntegrityCheckJob> logger,
        IGenericRepository<InitialPost, HistInitialPost, Guid> initialPostRepository,
        IUnitOfWork unitOfWork,
        DeletePhotoProducer deletePhotoProducer
    )
    {
        _logger = logger;
        _initialPostRepository = initialPostRepository;
        _unitOfWork = unitOfWork;
        _deletePhotoProducer = deletePhotoProducer;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var initialPostId = context.MergedJobDataMap.GetGuid("InitialPostId");
        var initialPostSpec = new FullSpecification<InitialPost>();
        initialPostSpec.AddFilter(x => x.Id == initialPostId);
        initialPostSpec.UseInclude(x => x.Include(y => y.InitialPostPhotos));
        var initialPost = (
            await _initialPostRepository
                .GetBySpecificationAsync(initialPostSpec, context.CancellationToken)
                .ConfigureAwait(false)
        ).FirstOrDefault();
        if (initialPost != null)
        {
            _logger.LogInformation($"Cleaning up initial post with id {initialPostId}");

            foreach (var initialPostPhoto in initialPost.InitialPostPhotos)
            {
                await _deletePhotoProducer
                    .Send(new DeletePhoto(initialPostPhoto.PhotoId), context.CancellationToken)
                    .ConfigureAwait(false);
            }

            await _initialPostRepository.DeleteAsync(initialPostId).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync(context.CancellationToken).ConfigureAwait(false);
        }
    }
}
