using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("CustomerId is required.");
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Items are required.")
                .Must(items => items.Count > 0)
                .WithMessage("At least one item is required.");
            RuleForEach(x => x.Items)
                .SetValidator(new CreateOrderItemRequestValidator());
        }
    }
}
