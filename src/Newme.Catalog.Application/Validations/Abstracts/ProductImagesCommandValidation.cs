using FluentValidation;
using Newme.Catalog.Application.Commands;

namespace Newme.Catalog.Application.Validations
{
    public abstract class ProductImagesCommandValidation : AbstractValidator<UploadProductImagesCommand>
    {
        protected void ValidateDescription()
        {
            RuleFor(c => c.Images)
                .NotNull()
                .NotEmpty()
                .WithMessage("Images not informed.");
        }
    }
}