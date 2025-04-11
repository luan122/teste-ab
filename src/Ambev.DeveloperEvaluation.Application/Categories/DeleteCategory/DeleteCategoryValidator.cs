using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory;

/// <summary>
/// Validator for DeleteCategoryCommand
/// </summary>
public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteCategoryCommand
    /// </summary>
    public DeleteCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Category ID is required");
    }
}
