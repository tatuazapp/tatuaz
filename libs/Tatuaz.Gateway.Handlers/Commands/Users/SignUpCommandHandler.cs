using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Tatuaz.Gateway.Requests.Commands.Users;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Identity;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Commands.Users;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, TatuazResult<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserAccessor _userAccessor;
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;

    public SignUpCommandHandler(
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserAccessor userAccessor
    )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<TatuazResult<UserDto>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new CreateUserDtoValidator(_userRepository)
            .ValidateAsync(request.CreateUserDto, cancellationToken).ConfigureAwait(false);

        if (!validationResult.IsValid) return CommonResultFactory.ValidationError<UserDto>(validationResult);

        var userId = _userAccessor.CurrentUserId ?? throw new InvalidOperationException("User context not available");

        if (await _userRepository.ExistsByIdAsync(userId, cancellationToken).ConfigureAwait(false))
            return CreateUserResultFactory.UserAlreadyExists<UserDto>();

        var user = _mapper.Map<TatuazUser>(request.CreateUserDto);
        user.Id = userId;
        _userRepository.Create(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return CommonResultFactory.Ok(_mapper.Map<UserDto>(user), HttpStatusCode.Created);
    }
}