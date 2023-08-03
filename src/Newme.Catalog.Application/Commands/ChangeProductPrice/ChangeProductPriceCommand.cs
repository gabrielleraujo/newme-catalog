using FluentValidation.Results;
using MediatR;

namespace Newme.Catalog.Application.Commands
{
    public class ChangeProductPriceCommand : Command, IRequest<ValidationResult>
    {
        public ChangeProductPriceCommand(Guid id, string currency, decimal amount)
        {
            Id = id;
            Currency = currency;
            Amount = amount;
        }

        public Guid Id { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}