using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Validator for ListProductCommand
/// </summary>
public class ListProductValidator : AbstractValidator<ListProductCommand>
{
    /// <summary>
    /// Initializes validation rules for ListProductCommand
    /// </summary>
    public ListProductValidator()
    {
        RuleFor(x => x.Page)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Size)
            .NotEmpty()
            .GreaterThanOrEqualTo(10);
    }
}
