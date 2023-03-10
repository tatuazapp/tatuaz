using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Consumers.Photo;
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

public class DeleteBackgroundPhotoConsumer : TatuazConsumerBase<DeleteBackgroundPhoto, EmptyDto>
{
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IUserContext _userContext;

    public DeleteBackgroundPhotoConsumer(
        ILogger<DeleteBackgroundPhotoConsumer> logger,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IUserContext userContext
    )
        : base(logger)
    {
        _userRepository = userRepository;
        _userContext = userContext;
    }

    protected override async Task<TatuazResult<EmptyDto>> ConsumeMessage(
        ConsumeContext<DeleteBackgroundPhoto> context
    )
    {
        var spec = new FullSpecification<TatuazUser>();
        spec.AddFilter(x => x.Id == _userContext.RequiredCurrentUserEmail());
        spec.UseInclude(x => x.Include(y => y.BackgroundPhoto)!);
        var user = (
            await _userRepository.GetBySpecificationAsync(spec).ConfigureAwait(false)
        ).Single();

        if (user.BackgroundPhoto == null)
        {
            return DeleteBackgroundPhotoResultFactory.BackgroundPhotoNotFound<EmptyDto>();
        }

        await context.Publish(new DeletePhoto(user.BackgroundPhoto.Id)).ConfigureAwait(false);

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
