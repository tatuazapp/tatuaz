using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Exceptions;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Users;

public class WhoAmIQueryHandler : IRequestHandler<WhoAmIQuery, TatuazResult<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IUserContext _userContext;

    public WhoAmIQueryHandler(
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
        WhoAmIQuery request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository
            .GetByIdAsync(
                _userContext.CurrentUserEmail ?? throw new UserContextMissingException(),
                cancellationToken: cancellationToken
            )
            .ConfigureAwait(false);
        return user == null
            ?
            // Authorization challenge should catch this
            CommonResultFactory.InternalError<UserDto>()
            : CommonResultFactory.Ok(_mapper.Map<UserDto>(user));
    }
}
