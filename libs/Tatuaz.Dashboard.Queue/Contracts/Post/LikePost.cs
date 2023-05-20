using System;

namespace Tatuaz.Dashboard.Queue.Contracts.Post;

public record LikePost(Guid PostId, bool Like);
