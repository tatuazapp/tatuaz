using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Tatuaz.Dashboard.Queue.Contracts.Photo;
using Tatuaz.Dashboard.Queue.Producers.Photo;
using Tatuaz.Gateway.Requests.Queries.Photo;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Handlers.Queries.Photo;

public class ListCategoriesQueryHandler
    : IRequestHandler<ListCategoriesQuery, TatuazResult<PagedData<CategoryDto>>>
{
    private readonly IValidator<ListCategoriesDto> _validator;
    private readonly ListCategoriesProducer _listCategoriesProducer;

    public ListCategoriesQueryHandler(
        IValidator<ListCategoriesDto> validator,
        ListCategoriesProducer listCategoriesProducer
    )
    {
        _validator = validator;
        _listCategoriesProducer = listCategoriesProducer;
    }

    public async Task<TatuazResult<PagedData<CategoryDto>>> Handle(
        ListCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validator
            .ValidateAsync(request.ListCategoriesDto, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return CommonResultFactory.ValidationError<PagedData<CategoryDto>>(validationResult);
        }

        return await _listCategoriesProducer
            .Send(
                new ListCategories(
                    request.ListCategoriesDto.PageNumber!.Value,
                    request.ListCategoriesDto.PageSize!.Value
                ),
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
