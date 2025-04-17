using Ambev.DeveloperEvaluation.Common.Validators;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(product => product.Category).Length(3, 50);
        RuleFor(product => product.Description).Length(10, 100);
        RuleFor(product => product.Image).SetValidator(new FormFileValidator(true));
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Rating).SetValidator(new ProductRateValidator(true));
        RuleFor(product => product.Title).NotEmpty().Length(3, 100);
    }
}

