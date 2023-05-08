using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
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

public class SetBioConsumer : TatuazConsumerBase<SetBio, EmptyDto>
{
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public SetBioConsumer(
        ILogger<SetBioConsumer> logger,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext
    )
        : base(logger)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    protected override async Task<TatuazResult<EmptyDto>> ConsumeMessage(
        ConsumeContext<SetBio> context
    )
    {
        var spec = new FullSpecification<TatuazUser>();
        spec.AddFilter(x => x.Id == _userContext.RequiredCurrentUserEmail());
        spec.TrackingStrategy = TrackingStrategy.Tracking;
        var user = (await _userRepository.GetBySpecificationAsync(spec)).Single();

        user.Bio = context.Message.Bio;

        await _unitOfWork.SaveChangesAsync();

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
