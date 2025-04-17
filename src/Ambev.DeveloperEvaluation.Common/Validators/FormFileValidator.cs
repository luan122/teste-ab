using ByteSizeLib;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Common.Validators
{
    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        public FormFileValidator(bool AcceptNull = false)
        {
            if (!AcceptNull)
            {
                RuleFor(x => x.Length)
                    .NotNull()
                    .Must(x =>
                    {
                        var kb = ByteSize.FromKiloBytes(10).Bytes;
                        var mb = ByteSize.FromMegaBytes(10).Bytes;
                        return x > kb && x <= mb;
                    })
                    .WithMessage("File size must be greater than 10 kb and less than 10MB.");
                RuleFor(x => x.ContentType)
                    .Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("image/webp") || x.Equals("image/svg+xml"))
                    .WithMessage("File type should be jpeg, jpg, png, webp or svg");
            }
            else
            {
                RuleFor(x => x.Length)
                    .Must(x =>
                    {
                        var kb = ByteSize.FromKiloBytes(10).Bytes;
                        var mb = ByteSize.FromMegaBytes(10).Bytes;
                        return (x > kb && x <= mb || x == 0);
                    })
                    .WithMessage("File size must be greater than 10 kb and less than 10MB.");
                RuleFor(x => x.ContentType)
                    .Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("image/webp") || x.Equals("image/svg+xml") || x == null || x == string.Empty)
                    .WithMessage("File type should be jpeg, jpg, png, webp or svg");
            }
        }
    }
}
