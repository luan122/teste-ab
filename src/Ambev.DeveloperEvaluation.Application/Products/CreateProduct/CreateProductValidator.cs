using Ambev.DeveloperEvaluation.Application.Products.Shared;
using Ambev.DeveloperEvaluation.Common.Validators;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for product creation command.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
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
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Title).NotEmpty().Length(3, 50);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Description).NotEmpty().Length(10,300);
        RuleFor(product => product.Category).NotEmpty().Length(3,50);
        RuleFor(product => product.Image).NotEmpty().SetValidator(new ProductImageCommandValidator());
        RuleFor(product => product.Rating)
            .NotEmpty()
            .SetValidator(new ProductRateCommandValidator())
            .When(product => product.Rating != null);
    }
}