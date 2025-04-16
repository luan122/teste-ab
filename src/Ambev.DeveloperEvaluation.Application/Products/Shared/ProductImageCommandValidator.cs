using Ambev.DeveloperEvaluation.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Shared
{
    public class ProductImageCommandValidator : AbstractValidator<ProductImageCommand>
    {
        public ProductImageCommandValidator(bool acceptNull = false)
        {
            if (!acceptNull)
            {
                RuleFor(x => x.ImageName)
                    .NotEmpty()
                    .WithMessage("Image name cannot be empty.")
                    .Must(url => url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                 url.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                 url.EndsWith(".webp", StringComparison.OrdinalIgnoreCase))
                    .WithMessage("Product image name must be of type jpg, jpeg, or webp.");
                RuleFor(x => x.File)
                    .SetValidator(new FormFileValidator());
            }
            else
            {
                RuleFor(x => x.ImageName)
                    .Must(url => url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                 url.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                 url.EndsWith(".webp", StringComparison.OrdinalIgnoreCase) ||
                                 string.IsNullOrWhiteSpace(url))
                    .WithMessage("Product image name must be of type jpg, jpeg, or webp.");
                RuleFor(x => x.File)
                    .SetValidator(new FormFileValidator(true));
            }
        }
    }
}
