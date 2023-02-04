using MediatR;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Requests.Queries.Photo;

public record ListPhotoCategoriesQuery(ListPhotoCategoriesDto ListPhotoCategoriesDto)
    : IRequest<TatuazResult<PagedData<PhotoCategoryDto>>>;
