using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Identity;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;

namespace Tatuaz.Dashboard.Queue.Consumers.Identity;

public class GetUserConsumer : TatuazConsumerBase<GetUser, UserDto>
{
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;

    public GetUserConsumer(
        ILogger<GetUserConsumer> logger,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository
    )
        : base(logger)
    {
        _userRepository = userRepository;
    }

    protected override async Task<TatuazResult<UserDto>> ConsumeMessage(
        ConsumeContext<GetUser> context
    )
    {
        var spec = new FullSpecification<TatuazUser>();
        spec.AddFilter(x => x.Username.ToLower() == context.Message.Username.ToLower());

        var userDto = (
            await _userRepository
                .GetBySpecificationAsync<UserDto>(spec, context.CancellationToken)
                .ConfigureAwait(false)
        ).SingleOrDefault();

        return userDto == null
            ? GetUserResultFactory.UserNotFound<UserDto>(context.Message.Username)
            : CommonResultFactory.Ok(userDto);
    }
}
