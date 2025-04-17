using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Shared
{
    public class ProductRateCommandValidator : AbstractValidator<ProductRateCommand>
    {
        public ProductRateCommandValidator(bool acceptNull = false)
        {
            if(!acceptNull)
                RuleFor(x => x.Rate)
                    .NotEmpty()
                    .WithMessage("Rate is required")
                    .InclusiveBetween(1, 5)
                    .WithMessage("Rate must be between 1 and 5");
            else
                RuleFor(x => x.Rate)
                    .InclusiveBetween(0, 5)
                    .WithMessage("Rate must be between 0 and 5");
        }
    }
}
