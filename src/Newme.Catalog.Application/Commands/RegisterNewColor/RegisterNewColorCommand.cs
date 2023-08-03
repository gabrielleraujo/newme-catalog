using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Validations;

namespace Newme.Catalog.Application.Commands
{
    public class RegisterNewColorCommand : DifferentialCommand, IRequest<ValidationResult>
    {
        public RegisterNewColorCommand(string name)
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