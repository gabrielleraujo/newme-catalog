using MediatR;
using FluentValidation.Results;
using Newme.Catalog.Application.Validations;

namespace Newme.Catalog.Application.Commands
{
    public class RegisterPromotionToProductCommand : PromotionCommand, IRequest<ValidationResult>
    {
        public RegisterPromotionToProductCommand(
            Guid productId,
            string name, 
            DateTime start, 
            DateTime end,
            string currency,
            decimal amount,
            decimal percentage, 
            string description)
        {
            ProductId = productId;
            Name = name;
            Start = start;
            End = end;
            Currency = currency;
            Amount = amount;
            Percentage = percentage;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterPromotionToProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}