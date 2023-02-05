using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Dashboard.Queue.Producers.Photo;
using Tatuaz.Gateway.Requests.Queries.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Photo;

public class ListPhotoCategoriesQueryHandler
    : IRequestHandler<ListPhotoCategoriesQuery, TatuazResult<PagedData<PhotoCategoryDto>>>
{
    private readonly IValidator<ListPhotoCategoriesDto> _validator;
    private readonly ListPhotoCategoriesProducer _listPhotoCategoriesProducer;

    public ListPhotoCategoriesQueryHandler(
        IValidator<ListPhotoCategoriesDto> validator,
        ListPhotoCategoriesProducer listPhotoCategoriesProducer
    )
    {
        _validator = validator;
        _listPhotoCategoriesProducer = listPhotoCategoriesProducer;
    }

    public async Task<TatuazResult<PagedData<PhotoCategoryDto>>> Handle(
        ListPhotoCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.ListPhotoCategoriesDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<PagedData<PhotoCategoryDto>>(
                validationResult
            );
        }

        return await _listPhotoCategoriesProducer
            .Send(
                new ListPhotoCategoriesOrder(
                    request.ListPhotoCategoriesDto.PageNumber.Value,
                    request.ListPhotoCategoriesDto.PageSize.Value
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
