using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newme.Catalog.Application.Validations;

namespace Newme.Catalog.Application.Commands
{
    public class UploadProductImagesCommand : Command, IRequest<ValidationResult>
    {
        public UploadProductImagesCommand(
            Guid productId,
            IList<IFormFile> images)
        {
            Id = productId;
            Images = images;
        }

        public Guid Id { get; private set; }
        public IList<IFormFile> Images { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UploadProductImagesCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
