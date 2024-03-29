using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Consumers.Photo;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Dashboard.Queue.Producers.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
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
    private readonly IUserContext _userContext;
    private readonly AddPhotoProducer _addPhotoProducer;

    public SetForegroundPhotoConsumer(
        ILogger<SetForegroundPhotoConsumer> logger,
        IUnitOfWork unitOfWork,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IUserContext userContext,
        AddPhotoProducer addPhotoProducer
    )
        : base(logger)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _userContext = userContext;
        _addPhotoProducer = addPhotoProducer;
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
            await context
                .Publish(new DeletePhoto(user.ForegroundPhotoId.Value))
                .ConfigureAwait(false);
        }

        var addPhotoResult = await _addPhotoProducer
            .Send(new AddPhoto(context.Message.Photo))
            .ConfigureAwait(false);

        if (!addPhotoResult.Successful)
        {
            return CommonResultFactory.InternalError<EmptyDto>("Failed to add photo");
        }

        user.ForegroundPhotoId = addPhotoResult.Value;
        await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
