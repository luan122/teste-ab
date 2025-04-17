using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

/// <summary>
/// Command for creating a new order.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a order, 
/// including ordername, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateOrderResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateOrderCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateOrderCommand : IRequest<CreateOrderResult>
{
    public Guid CustomerId { get; set; } = Guid.Empty;
    public Guid BranchId { get; set; } = Guid.Empty;
    public virtual List<CreateOrderItemCommand> Items { get; set; } = []!;

    public ValidationResultDetail Validate()
    {
        var validator = new CreateOrderCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}