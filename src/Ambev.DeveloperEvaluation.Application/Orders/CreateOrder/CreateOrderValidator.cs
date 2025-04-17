using Ambev.DeveloperEvaluation.Common.Validators;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

/// <summary>
/// Validator for CreateOrderCommand that defines validation rules for order creation command.
/// </summary>
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateOrderCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Ordername: Required, must be between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// </remarks>
    public CreateOrderCommandValidator()
    {
        RuleFor(order => order.Items).NotEmpty();
        RuleForEach(order => order.Items).SetValidator(new CreateOrderItemValidator());
        RuleFor(order => order.CustomerId).NotEmpty();
        RuleFor(order => order.BranchId).NotEmpty();
    }
}