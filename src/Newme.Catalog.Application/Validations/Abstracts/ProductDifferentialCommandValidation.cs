using FluentValidation;
using Newme.Catalog.Application.Commands;

namespace Newme.Catalog.Application.Validations
{
    public abstract class ProductDifferentialCommandValidation<T> : AbstractValidator<T> where T : DifferentialCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(1, 150).WithMessage("The Name must have between 1 and 150 characters");
        }
    }
}