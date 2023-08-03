using MediatR;
using FluentValidation.Results;

namespace Newme.Catalog.Application.Commands
{
    public class RegisterNewProductByCodeCommand : Command, IRequest<ValidationResult>
    {
        public RegisterNewProductByCodeCommand(
            Guid sku,
            Guid color, 
            Guid size,
            int quantity)
        {
            Sku = sku;
            SizeId = size;
            Stock = quantity;
        }

        public Guid Sku { get; private set; }
        public Guid ColorId { get; private set; }
        public Guid SizeId { get; private set; }
        public int Stock { get; private set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}