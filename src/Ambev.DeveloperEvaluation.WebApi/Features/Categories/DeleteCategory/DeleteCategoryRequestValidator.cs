﻿using Ambev.DeveloperEvaluation.WebApi.Features.Categories.DeleteCategory;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.DeleteCategory;

/// <summary>
/// Validator for DeleteCategoryRequest
/// </summary>
public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteCategoryRequest
    /// </summary>
    public DeleteCategoryRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Category ID is required");
    }
}