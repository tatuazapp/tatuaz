using AutoMapper;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Dtos;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();
    }
}
