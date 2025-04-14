using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Categories.GetCategory;

/// <summary>
/// Validator for GetCategoryCommand
/// </summary>
public class GetCategoryValidator : AbstractValidator<GetCategoryCommand>
{
    /// <summary>
    /// Initializes validation rules for GetCategoryCommand
    /// </summary>
    public GetCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Category ID is required");
    }
}
