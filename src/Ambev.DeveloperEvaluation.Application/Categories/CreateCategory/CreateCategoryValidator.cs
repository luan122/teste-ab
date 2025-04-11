using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;

/// <summary>
/// Validator for CreateUserCommand that defines validation rules for user creation command.
/// </summary>
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <list type="bullet">
    /// <item>
    /// <term>Title</term>
    /// <description>Required, must be between 3 and 50 characters</description>
    /// </item>
    /// </list>
    /// </remarks>
    public CreateCategoryCommandValidator()
    {
        RuleFor(category => category.Title).NotEmpty().Length(3, 50);
    }
}