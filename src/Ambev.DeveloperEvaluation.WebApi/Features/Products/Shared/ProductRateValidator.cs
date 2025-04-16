using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared
{
    public class ProductRateValidator : AbstractValidator<ProductRateRequest>
    {
        public ProductRateValidator(bool Acceptnull = false)
        {
            if(!Acceptnull)
                RuleFor(rating => rating.Rate).GreaterThan(0);

        }
    }
}
