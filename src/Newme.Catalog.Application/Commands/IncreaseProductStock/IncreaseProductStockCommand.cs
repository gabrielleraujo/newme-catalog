using FluentValidation.Results;
using MediatR;

namespace Newme.Catalog.Application.Commands
{
    public class IncreaseProductStockCommand : Command, IRequest<ValidationResult>
    {
        public IncreaseProductStockCommand(Guid id, int quantity)
        {
            Id = id;
            Stock = quantity;
        }

        public Guid Id { get; set; }
        public int Stock { get; set; }
    }
}