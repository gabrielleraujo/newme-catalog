using MediatR;
using FluentValidation.Results;
using Newme.Catalog.Application.Validations;

namespace Newme.Catalog.Application.Commands
{
    public class RegisterNewProductCommand : ProductCommand, IRequest<ValidationResult>
    {
        public RegisterNewProductCommand(
            string name, 
            string currency,
            decimal amount, 
            string description, 
            Guid genderId,
            Guid category, 
            Guid color, 
            Guid size, 
            int quantity)
        {
            Name = name;
            Currency = currency;
            Amount = amount;
            Description = description;
            GenderId = genderId;
            CategoryId = category;
            ColorId = color;
            SizeId = size;
            Stock = quantity;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}