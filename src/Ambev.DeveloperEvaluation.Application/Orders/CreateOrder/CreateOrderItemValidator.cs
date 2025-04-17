using Ambev.DeveloperEvaluation.Common.Validators;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

/// <summary>
/// Validator for CreateOrderItemCommand that defines validation rules for order item creation.
/// </summary>
public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateOrderItemValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductId: Must not be empty (required)
    /// - Quantity: Must not be empty (required)
    /// </remarks>
    public CreateOrderItemValidator()
    {
        RuleFor(order => order.ProductId).NotEmpty();
        RuleFor(order => order.Quantity)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(20);
    }
}
