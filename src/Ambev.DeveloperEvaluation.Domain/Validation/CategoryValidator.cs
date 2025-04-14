using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(category => category.Title)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Title cannot be longer than 50 characters.");
    }
}
