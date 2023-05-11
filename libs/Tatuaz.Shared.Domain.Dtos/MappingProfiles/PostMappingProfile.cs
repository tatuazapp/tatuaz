using System.Linq;
using AutoMapper;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Dtos.MappingProfiles;

public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Post, BriefPostDto>()
            .ConstructUsing(
                src =>
                    new BriefPostDto(
                        src.Id,
                        src.Description,
                        src.Photos.Select(x => x.Photo.Uri).ToArray(),
                        src.Author.Username,
                        src.Author.ForegroundPhoto != null ? src.Author.ForegroundPhoto.Uri : null,
                        src.Likes.Count,
                        src.Comments.Count,
                        src.CreatedAt
                    )
            );
    }
}
