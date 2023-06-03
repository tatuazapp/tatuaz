using FluentValidation;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post;
using Tatuaz.Shared.Domain.Dtos.Dtos.Post.GetPostDetails;
using Tatuaz.Shared.Pipeline.Factories.ErrorCodes.Post;

namespace Tatuaz.Shared.Domain.Dtos.Validators.Post;

public class GetPostDetailsDtoValidator : AbstractValidator<GetPostDetailsDto>
{
    public GetPostDetailsDtoValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull()
            .WithErrorCode(GetPostDetailsErrorCodes.PostIdIsNull)
            .WithMessage("PostId cannot be null");
    }
}
