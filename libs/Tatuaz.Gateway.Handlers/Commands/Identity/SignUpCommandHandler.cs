using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Tatuaz.Gateway.Requests.Commands.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Photo;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Pipeline.Exceptions;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Identity;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Gateway.Handlers.Commands.Identity;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, TatuazResult<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IGenericRepository<
        UserCategory,
        HistUserCategory,
        int
    > _userCategoryRepository;
    private readonly IUserContext _userContext;
    private readonly IValidator<SignUpDto> _validator;

    public SignUpCommandHandler(
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IGenericRepository<UserCategory, HistUserCategory, int> userCategoryRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserContext userContext,
        IValidator<SignUpDto> validator
    )
    {
        _userRepository = userRepository;
        _userCategoryRepository = userCategoryRepository;
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
            .ValidateAsync(request.SignUpDto, cancellationToken)
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
            return SignUpResultFactory.UserAlreadyExists<UserDto>();
        }

        var user = _mapper.Map<TatuazUser>(request.SignUpDto);
        user.Id = userEmail;
        user.Auth0Id = _userContext.CurrentUserAuth0Id ?? throw new UserContextMissingException();
        user.ForegroundPhotoId = null;
        user.BackgroundPhotoId = null;
        user.UserRoles = new List<TatuazUserRole>();
        user.Popularity = 0;
        await _unitOfWork
            .RunInTransactionAsync(
                _ =>
                {
                    _userRepository.Create(user);
                    foreach (var categoryId in request.SignUpDto.CategoryIds!)
                    {
                        var userCategory = new UserCategory
                        {
                            UserId = user.Id,
                            CategoryId = categoryId
                        };
                        _userCategoryRepository.Create(userCategory);
                    }
                    return Task.CompletedTask;
                },
                e => throw e,
                cancellationToken
            )
            .ConfigureAwait(false);

        return CommonResultFactory.Ok(_mapper.Map<UserDto>(user), HttpStatusCode.Created);
    }
}
