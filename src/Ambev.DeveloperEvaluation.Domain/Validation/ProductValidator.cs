using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Title)
                .NotEmpty()
                .WithMessage("Product title is required.")
                .Length(3, 100)
                .WithMessage("Product title must be between 3 and 100 characters.");

            RuleFor(product => product.Price)
                .NotEmpty()
                .WithMessage("Product price is required.")
                .GreaterThan(0)
                .WithMessage("Product price must be greater than 0.");

            RuleFor(product => product.Description)
                .NotEmpty()
                .WithMessage("Product description is required.")
                .MinimumLength(10)
                .WithMessage("Product description must be at least 10 characters long.");
                


        }
    }
}
