using FluentValidation;
using Newme.Catalog.Application.Commands;

namespace Newme.Catalog.Application.Validations
{
    public abstract class PromotionToProductCommandValidation<T> : AbstractValidator<T> where T : PromotionCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }

        protected void ValidateDescription()
        {
            RuleFor(c => c.Description)
               .NotEmpty()
               .Length(2, 150)
               .WithMessage("The description must have at least 2 caracters");
        }

        protected void Validatestart()
        {
            RuleFor(c => c.Start)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("The promotion cannot start on the past");
        }

        protected void ValidatePercentage()
        {
            RuleFor(c => c.Percentage)
                .GreaterThan(0.001m)
                .WithMessage("The percentage discount must be greater than 0.001 percent for a product");
        }

        protected void ValidateCurrency()
        {
            RuleFor(c => c.Currency.Length)
                .GreaterThan(0)
                .WithMessage("The currency is outside our acceptable range for a product");
        }
        
        protected void ValidateAmount()
        {
            RuleFor(c => c.Amount)
                .GreaterThan(0)
                .WithMessage("The amount is outside our acceptable range for a product");
        }
    }
}