using AutoMapper;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Dtos.MappingProfiles;

public class IdentityMappingProfile : Profile
{
    public IdentityMappingProfile()
    {
        CreateMap<TatuazUser, UserDto>();
        CreateMap<CreateUserDto, TatuazUser>()
            .ForMember(x => x.Email, opt => opt.MapFrom(q => q.Email!.ToLower()));
        CreateMap<TatuazUser, CreateUserDto>();
    }
}
