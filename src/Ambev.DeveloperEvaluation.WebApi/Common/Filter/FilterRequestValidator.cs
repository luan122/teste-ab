using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Common.Filter
{
    public class FilterRequestValidator : AbstractValidator<FilterRequest>
    {
        public FilterRequestValidator()
        {
            RuleFor(filter => filter);
        }
    }
}
