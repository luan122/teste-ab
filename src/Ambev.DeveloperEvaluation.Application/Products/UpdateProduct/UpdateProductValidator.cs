using Ambev.DeveloperEvaluation.Application.Products.Shared;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand that defines validation rules for product creation command.
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Productname: Required, must be between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// </remarks>
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
        RuleFor(product => product.Title).Length(3, 50);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Description).Length(10, 300);
        RuleFor(product => product.Category).Length(3, 50);
        RuleFor(product => product.Image).SetValidator(new ProductImageCommandValidator(true));
        RuleFor(product => product.Rating)
            .SetValidator(new ProductRateCommandValidator(true))
            .When(product => product.Rating != null);
    }
}