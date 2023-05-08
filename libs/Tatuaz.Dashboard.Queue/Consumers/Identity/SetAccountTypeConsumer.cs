using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Identity;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Dashboard.Queue.Consumers.Identity;

public class SetAccountTypeConsumer : TatuazConsumerBase<SetAccountType, EmptyDto>
{
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public SetAccountTypeConsumer(
        ILogger<SetAccountTypeConsumer> logger,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork
    )
        : base(logger)
    {
        _userRepository = userRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<TatuazResult<EmptyDto>> ConsumeMessage(
        ConsumeContext<SetAccountType> context
    )
    {
        var spec = new FullSpecification<TatuazUser>();
        spec.AddFilter(x => x.Id == _userContext.RequiredCurrentUserEmail());
        spec.UseInclude(x => x.Include(y => y.UserRoles));
        spec.TrackingStrategy = TrackingStrategy.Tracking;
        var user = (
            await _userRepository.GetBySpecificationAsync(spec).ConfigureAwait(false)
        ).Single();

        if (context.Message.Artist)
        {
            if (user.UserRoles.Any(x => x.RoleId == TatuazRole.ArtistId))
            {
                return SetAccountTypeResultFactory.AlreadyArtist<EmptyDto>();
            }

            user.UserRoles.Add(
                new TatuazUserRole { RoleId = TatuazRole.ArtistId, UserEmail = user.Id }
            );
        }
        else
        {
            if (user.UserRoles.All(x => x.RoleId != TatuazRole.ArtistId))
            {
                return SetAccountTypeResultFactory.AlreadyClient<EmptyDto>();
            }

            user.UserRoles.Remove(user.UserRoles.Single(x => x.RoleId == TatuazRole.ArtistId));
        }

        await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
