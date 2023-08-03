using FluentValidation.Results;
using MediatR;
using Newme.Catalog.Application.Subscribers.PurchaseOrderCreated.Received;

namespace Newme.Catalog.Application.Commands
{
    public class ReduceProductsStockCommand : Command, IRequest<ValidationResult>
    {
        public ReduceProductsStockCommand(CreatedPurchaseOrderReceivedEvent @event)
        {
            Event = @event;
        }

        public CreatedPurchaseOrderReceivedEvent Event { get; set; }
    }
}