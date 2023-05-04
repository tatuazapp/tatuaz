using System;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;

namespace Tatuaz.Dashboard.Queue.Contracts.Post;

public record FinalizePost(Guid InitialPostId, string Description, PhotoInfoDto[] Photo);
