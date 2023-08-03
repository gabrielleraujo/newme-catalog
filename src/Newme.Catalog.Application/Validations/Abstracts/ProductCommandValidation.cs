using FluentValidation;
using Newme.Catalog.Application.Commands;

namespace Newme.Catalog.Application.Validations
{
    public abstract class ProductCommandValidation<T> : AbstractValidator<T> where T : ProductCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
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

        protected void ValidateDescription()
        {
            RuleFor(c => c.Description)
               .NotEmpty()
               .Length(2, 500);
        }

        protected void ValidateStock()
        {
            RuleFor(c => c.Stock)
                .GreaterThan(0)
                .WithMessage("Please, make sure you have entered the stock quantity for this product");
        }
    }
}