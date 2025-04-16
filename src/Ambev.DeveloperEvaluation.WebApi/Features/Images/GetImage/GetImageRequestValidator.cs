using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Images.GetImage
{
    public class GetImageRequestValidator : AbstractValidator<GetImageRequest>
    {
        public GetImageRequestValidator()
        {
            RuleFor(x => x.ImageName)
                .NotEmpty()
                .WithMessage("Image name is required.");
        }
    }
}
