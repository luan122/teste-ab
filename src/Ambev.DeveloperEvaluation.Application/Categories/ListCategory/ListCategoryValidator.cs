using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Categories.ListCategory;

/// <summary>
/// Validator for ListCategoryCommand
/// </summary>
public class ListCategoryValidator : AbstractValidator<ListCategoryCommand>
{
    /// <summary>
    /// Initializes validation rules for ListCategoryCommand
    /// </summary>
    public ListCategoryValidator()
    {
        RuleFor(x => x.Page)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Size)
            .NotEmpty()
            .GreaterThanOrEqualTo(10);
    }
}
