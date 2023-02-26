using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.Category;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Photo;

public record ListCategoriesQuery(ListCategoriesDto ListCategoriesDto)
    : IRequest<TatuazResult<PagedData<CategoryDto>>>;
