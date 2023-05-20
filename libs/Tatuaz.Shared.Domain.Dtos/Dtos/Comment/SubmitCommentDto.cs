using System;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Comment;

public record SubmitCommentDto(Guid PostId, Guid? ParentCommentId, string Content) : IDto;
