using AutoMapper;
using MediatR;
using Tatuaz.Gateway.Infrastructure;
using Tatuaz.Gateway.Requests.Commands.Users;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Users;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, TatuazResult<UserDto>>
{
    private readonly IGenericRepository<GatewayDbContext, TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IUnitOfWork<GatewayDbContext> _unitOfWork;
    private readonly IMapper _mapper;

    public SignUpCommandHandler(
        IGenericRepository<GatewayDbContext, TatuazUser, HistTatuazUser, string> userRepository,
        IUnitOfWork<GatewayDbContext> unitOfWork,
        IMapper mapper
    )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TatuazResult<UserDto>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new CreateUserDtoValidator<GatewayDbContext>(_userRepository)
            .ValidateAsync(request.CreateUserDto, cancellationToken).ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<UserDto>(validationResult);
        }

        var user = _mapper.Map<TatuazUser>(request.CreateUserDto);
        _userRepository.Create(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return CommonResultFactory.Ok(_mapper.Map<UserDto>(user));
    }
}
