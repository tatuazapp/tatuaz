using AutoMapper;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity;
using Tatuaz.Shared.Domain.Dtos.Dtos.Identity.User;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Dtos.MappingProfiles;

public class IdentityMappingProfile : Profile
{
    public IdentityMappingProfile()
    {
        CreateMap<TatuazUser, UserDto>()
            .ConstructUsing(
                x =>
                    new UserDto(
                        x.Username,
                        x.Id,
                        x.Auth0Id,
                        x.ForegroundPhoto != null ? x.ForegroundPhoto.Uri : null,
                        x.BackgroundPhoto != null ? x.BackgroundPhoto.Uri : null
                    )
            );
        CreateMap<SignUpDto, TatuazUser>();
        CreateMap<TatuazUser, SignUpDto>();
    }
}
