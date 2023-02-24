using AutoMapper;
using Tatuaz.Shared.Domain.Dtos.Dtos.Photo.PhotoCategory;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Dtos.MappingProfiles;

public class PhotoMappingProfile : Profile
{
    public PhotoMappingProfile()
    {
        CreateMap<Category, PhotoCategoryDto>();
    }
}
