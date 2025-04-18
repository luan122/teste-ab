using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrderItem
{
    internal class CancelOrderItemCommandValidator : AbstractValidator<CancelOrderItemCommand>
    {
        public CancelOrderItemCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .Must(BothSet)
                .WithMessage("Can not provide both parameters, must be Int or Guid")
                .Must(BeAValidGuidIdWhenOrderNumberIsNotSet)
                .WithMessage("Id must be a valid Int or Guid value, if Int need to be greater than 0");

            RuleFor(x => x.OrderNumber)
                .Must(BothSet)
                .WithMessage("Can not provide both parameters, must be Int or Guid")
                .Must(BeValidIntOrderNumberWhenIdIsNotSet)
                .WithMessage("Id must be a valid Int or Guid value, if Int need to be greater than 0");

            RuleFor(x => x.OrderItemId)
                .NotEmpty()
                .WithMessage("OrderId is required");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Fail to identify the user")
                .WithName("Authentication");
        }
        private static bool BothSet<T>(CancelOrderItemCommand obj, T _)
        {
            if (obj.OrderId != Guid.Empty && obj.OrderNumber > 0)
                return false;
            return true;
        }
        private static bool BeAValidGuidIdWhenOrderNumberIsNotSet(CancelOrderItemCommand obj, Guid id)
        {
            if (id == Guid.Empty && obj.OrderNumber <= 0)
                return false;
            return true;
        }
        private static bool BeValidIntOrderNumberWhenIdIsNotSet(CancelOrderItemCommand obj, int orderNumber)
        {
            if (orderNumber <= 0 && obj.OrderId == Guid.Empty)
                return false;
            return true;
        }
    }
}
