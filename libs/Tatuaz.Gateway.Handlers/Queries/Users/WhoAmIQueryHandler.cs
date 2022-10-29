using AutoMapper;
using MediatR;
using Tatuaz.Gateway.Infrastructure;
using Tatuaz.Gateway.Requests.Queries.Users;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Users;

public class WhoAmIQueryHandler : IRequestHandler<WhoAmIQuery, TatuazResult<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<GatewayDbContext, TatuazUser, HistTatuazUser, string> _userRepository;

    public WhoAmIQueryHandler(
        IMapper mapper,
        IGenericRepository<GatewayDbContext, TatuazUser, HistTatuazUser, string> userRepository
    )
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<TatuazResult<UserDto>> Handle(WhoAmIQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByIdAsync(request.UserId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        if (user == null)
        {
            // Authorization challenge should catch this
            return CommonResultFactory.InternalError<UserDto>();
        }

        return CommonResultFactory.Ok(_mapper.Map<UserDto>(user));
    }
}
