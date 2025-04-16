using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Images.GetImage
{
    public class GetImageValidator : AbstractValidator<GetImageCommand>
    {
        public GetImageValidator()
        {
            RuleFor(x => x.ImageName)
                .NotEmpty();
        }
    }
}
