using MediatR;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Events
{
    public class RemoveProductEvent : INotification
    {
        public Guid Id { get; }
        public ProductId ProductId { get; }
        public string Detail { get; }

        public RemoveProductEvent(ProductId productId, string detail)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Detail = detail;
        }
    }
}