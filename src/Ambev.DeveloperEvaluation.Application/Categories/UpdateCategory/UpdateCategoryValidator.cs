using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Categories.UpdateCategory;

/// <summary>
/// Validator for UpdateCategoryCommand that defines validation rules for user creation command.
/// </summary>
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateCategoryValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <list type="bullet">
    /// <item>
    /// <term>Id</term>
    /// <description>Required, not default Guid</description>
    /// </item>
    /// <item>
    /// <term>Title</term>
    /// <description>Required, must be between 3 and 50 characters</description>
    /// </item>
    /// </list>
    /// </remarks>
    public UpdateCategoryCommandValidator()
    {
        RuleFor(category => category.Id).NotEmpty().WithMessage("Id is required").Must(m => m != Guid.Empty).WithMessage("Id cant be default Guid");
        RuleFor(category => category.Title).NotEmpty().WithMessage("Title is required").Length(3, 50).WithMessage("Title must be between 3 and 50 characters");
    }
}