using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Tatuaz.Gateway.Requests.Commands.Users;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Validators.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Exceptions;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Identity;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Gateway.Handlers.Commands.Users;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, TatuazResult<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IUserContext _userContext;
    private readonly IValidator<CreateUserDto> _validator;

    public SignUpCommandHandler(
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserContext userContext,
        IValidator<CreateUserDto> validator
    )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userContext = userContext;
        _validator = validator;
    }

    public async Task<TatuazResult<UserDto>> Handle(
        SignUpCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.CreateUserDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<UserDto>(validationResult);
        }

        var userEmail = _userContext.CurrentUserEmail ?? throw new UserContextMissingException();

        if (
            await _userRepository
                .ExistsByIdAsync(userEmail, cancellationToken)
                .ConfigureAwait(false)
        )
        {
            return CreateUserResultFactory.UserAlreadyExists<UserDto>();
        }

        var user = _mapper.Map<TatuazUser>(request.CreateUserDto);
        user.Id = userEmail;
        user.Auth0Id = _userContext.CurrentUserAuth0Id ?? throw new UserContextMissingException();
        _userRepository.Create(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return CommonResultFactory.Ok(_mapper.Map<UserDto>(user), HttpStatusCode.Created);
    }
}
