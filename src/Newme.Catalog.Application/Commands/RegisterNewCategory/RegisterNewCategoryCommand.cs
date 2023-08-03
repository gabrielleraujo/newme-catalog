using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Validations;

namespace Newme.Catalog.Application.Commands
{
    public class RegisterNewCategoryCommand : DifferentialCommand, IRequest<ValidationResult>
    {
        public RegisterNewCategoryCommand(string name)
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