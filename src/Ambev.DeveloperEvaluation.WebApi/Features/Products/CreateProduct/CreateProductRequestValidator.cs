using Ambev.DeveloperEvaluation.Common.Validators;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(product => product.Category).NotEmpty().Length(3, 50);
            RuleFor(product => product.Description).NotEmpty().Length(10,100);
            RuleFor(product => product.Image).SetValidator(new FormFileValidator());
            RuleFor(product => product.Price).GreaterThan(0);
            RuleFor(product => product.Rating).SetValidator(new ProductRateValidator());
            RuleFor(product => product.Title).NotEmpty().Length(3,100);
        }
    }
}
