using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Tatuaz.Gateway.Requests.Queries.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Exceptions;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Gateway.Handlers.Queries.Identity;

public class MeQueryHandler : IRequestHandler<MeQuery, TatuazResult<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IUserContext _userContext;

    public MeQueryHandler(
        IMapper mapper,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IUserContext userContext
    )
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userContext = userContext;
    }

    public async Task<TatuazResult<UserDto>> Handle(
        MeQuery request,
        CancellationToken cancellationToken
    )
    {
        var userDto = await _userRepository
            .GetByIdAsync<UserDto>(
                _userContext.RequiredCurrentUserEmail(),
                cancellationToken: cancellationToken
            )
            .ConfigureAwait(false);
        return userDto == null
            ?
            // Authorization challenge should catch this
            CommonResultFactory.InternalError<UserDto>()
            : CommonResultFactory.Ok(userDto);
    }
}
