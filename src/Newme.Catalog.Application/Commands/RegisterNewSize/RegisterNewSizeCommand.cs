using MediatR;
using FluentValidation.Results;
using Newme.Catalog.Application.Validations;

namespace Newme.Catalog.Application.Commands
{
    public class RegisterNewSizeCommand : DifferentialCommand, IRequest<ValidationResult>
    {
        public RegisterNewSizeCommand(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewProductDifferentialCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}