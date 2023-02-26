using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Blob;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Dashboard.Queue.Consumers.Identity;

public class SetForegroundPhotoConsumer : TatuazConsumerBase<SetForegroundPhoto, EmptyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IGenericRepository<
        Shared.Domain.Entities.Models.Photo.Photo,
        HistPhoto,
        Guid
    > _photoRepository;
    private readonly IUserContext _userContext;

    public SetForegroundPhotoConsumer(
        ILogger<SetForegroundPhotoConsumer> logger,
        IUnitOfWork unitOfWork,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IGenericRepository<
            Shared.Domain.Entities.Models.Photo.Photo,
            HistPhoto,
            Guid
        > photoRepository,
        IUserContext userContext
    )
        : base(logger)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _photoRepository = photoRepository;
        _userContext = userContext;
    }

    protected override async Task<TatuazResult<EmptyDto>> ConsumeMessage(
        ConsumeContext<SetForegroundPhoto> context
    )
    {
        var spec = new FullSpecification<TatuazUser>();
        spec.AddFilter(x => x.Id == _userContext.RequiredCurrentUserEmail());
        spec.TrackingStrategy = TrackingStrategy.Tracking;
        spec.UseInclude(x => x.Include(y => y.ForegroundPhoto)!);
        var user = (
            await _userRepository.GetBySpecificationAsync(spec).ConfigureAwait(false)
        ).Single();

        if (user.ForegroundPhotoId != null)
        {
            var photoId = user.ForegroundPhotoId.Value;
            await _photoRepository.DeleteAsync(photoId).ConfigureAwait(false);
            await context.Publish(new DeleteBlobFile(photoId)).ConfigureAwait(false);
        }

        var photo = new Shared.Domain.Entities.Models.Photo.Photo();
        _photoRepository.Create(photo);
        user.ForegroundPhoto = photo;
        await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        await context
            .Publish(new AddBlobFile(photo.Id, context.Message.Photo))
            .ConfigureAwait(false);
        return CommonResultFactory.Ok(new EmptyDto());
    }
}
