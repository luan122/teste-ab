using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrder
{
    internal class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
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

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Fail to identify the user")
                .WithName("Authentication");
        }
        private static bool BothSet<T>(CancelOrderCommand obj, T _)
        {
            if (obj.Id != Guid.Empty && obj.OrderNumber > 0)
                return false;
            return true;
        }
        private static bool BeAValidGuidIdWhenOrderNumberIsNotSet(CancelOrderCommand obj, Guid id)
        {
            if (id == Guid.Empty && obj.OrderNumber <= 0)
                return false;
            return true;
        }
        private static bool BeValidIntOrderNumberWhenIdIsNotSet(CancelOrderCommand obj, int orderNumber)
        {
            if (orderNumber <= 0 && obj.Id == Guid.Empty)
                return false;
            return true;
        }
    }
}
