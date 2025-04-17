using Ambev.DeveloperEvaluation.Common.Validators;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.GetOrder;

/// <summary>
/// Validator for GetOrderCommand that defines validation rules for order creation command.
/// </summary>
public class GetOrderCommandValidator : AbstractValidator<GetOrderCommand>
{
    /// <summary>
    /// Initializes a new instance of the GetOrderCommandValidator with defined validation rules.
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
    public GetOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .Must(BothSet)
            .WithMessage("Can not provide both parameters, must be Int or Guid")
            .Must(BeAValidGuidIdWhenOrderNumberIsNotSet)
            .WithMessage("Id must be a valid Int or Guid value, if Int need to be greater than 0");

        RuleFor(x => x.OrderNumber)
            .Must(BothSet)
            .WithMessage("Can not provide both parameters, must be Int or Guid")
            .Must(BeValidIntOrderNumberWhenIdIsNotSet)
            .WithMessage("Id must be a valid Int or Guid value, if Int need to be greater than 0");
    }
    private static bool BothSet<T>(GetOrderCommand obj, T _)
    {
        if (obj.Id != Guid.Empty && obj.OrderNumber > 0)
            return false;
        return true;
    }
    private static bool BeAValidGuidIdWhenOrderNumberIsNotSet(GetOrderCommand obj, Guid id)
    {
        if (id == Guid.Empty && obj.OrderNumber <= 0)
            return false;
        return true;
    }
    private static bool BeValidIntOrderNumberWhenIdIsNotSet(GetOrderCommand obj, int orderNumber)
    {
        if (orderNumber <= 0 && obj.Id == Guid.Empty)
            return false;
        return true;
    }
}