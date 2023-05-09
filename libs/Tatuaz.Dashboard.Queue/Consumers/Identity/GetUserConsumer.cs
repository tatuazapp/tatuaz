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
    private readonly IUnitOfWork _unitOfWork;

    public GetUserConsumer(
        ILogger<GetUserConsumer> logger,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IUnitOfWork unitOfWork
    )
        : base(logger)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<TatuazResult<UserDto>> ConsumeMessage(
        ConsumeContext<GetUser> context
    )
    {
        var spec = new FullSpecification<TatuazUser>();
        spec.AddFilter(
            x =>
                string.Equals(
                    x.Username,
                    context.Message.Username,
                    StringComparison.CurrentCultureIgnoreCase
                )
        );

        var user = (
            await _userRepository
                .GetBySpecificationAsync(spec, context.CancellationToken)
                .ConfigureAwait(false)
        ).SingleOrDefault();

        if (user == null)
        {
            return GetUserResultFactory.UserNotFound<UserDto>(context.Message.Username);
        }

        user.Popularity++;
        await _unitOfWork.SaveChangesAsync(context.CancellationToken).ConfigureAwait(false);

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
