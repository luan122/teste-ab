using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.GetOrder;

/// <summary>
/// Command for retrieving an existing order by its ID.
/// </summary>
/// <remarks>
/// This command is used to request the details of an existing order based on its unique identifier.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="GetOrderResult"/> containing the order details.
/// 
/// The request is validated using the <see cref="GetOrderCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the ID field is correctly provided.
/// </remarks>
public class GetOrderCommand : IRequest<GetOrderResult>
{
    public Guid Id { get; set; } = Guid.Empty;
    public int OrderNumber { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new GetOrderCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}