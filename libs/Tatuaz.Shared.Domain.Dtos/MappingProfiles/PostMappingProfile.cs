using System.Linq;
using AutoMapper;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post.GetPostDetails;
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
                        false,
                        src.Comments.Count,
                        src.CreatedAt
                    )
            );
        CreateMap<Post, PostDetailsDto>()
            .ConstructUsing(
                src =>
                    new PostDetailsDto(
                        src.Id,
                        src.Description,
                        src.Author.Username,
                        src.Author.ForegroundPhoto != null ? src.Author.ForegroundPhoto.Uri : null,
                        src.Likes.Count,
                        src.Likes.Any(x => x.UserId == src.AuthorId),
                        src.CreatedAt,
                        src.Comments
                            .Select(
                                x =>
                                    new PostCommentDto(
                                        x.Id,
                                        x.ParentCommentId,
                                        x.Content,
                                        x.User.Username,
                                        x.User.ForegroundPhoto != null
                                            ? x.User.ForegroundPhoto.Uri
                                            : null,
                                        x.Likes.Count,
                                        x.Likes.Any(y => y.UserId == src.AuthorId),
                                        x.CreatedAt
                                    )
                            )
                            .ToList()
                    )
            );
        CreateMap<PostPhoto, PostPhotoDto>()
            .ConstructUsing(
                src =>
                    new PostPhotoDto(
                        src.Photo.Uri,
                        src.Photo.PhotoCategories.Select(c => c.CategoryId).ToArray()
                    )
            );
    }
}
