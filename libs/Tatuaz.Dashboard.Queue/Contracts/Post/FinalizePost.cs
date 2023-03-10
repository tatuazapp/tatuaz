using Tatuaz.Shared.Domain.Dtos.Dtos.Post;

namespace Tatuaz.Dashboard.Queue.Contracts.Photo;

public record FinalizePost(string Description, PhotoInfoDto[] Photo);
