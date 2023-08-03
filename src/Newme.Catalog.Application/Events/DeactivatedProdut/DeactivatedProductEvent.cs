using MediatR;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Events
{
    public class DeactivatedProductEvent : INotification
    {
        public Guid Id { get; }
        public ProductId ProductId { get; }
        public string Detail { get; }

        public DeactivatedProductEvent(ProductId productId, string detail)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Detail = detail;
        }
    }
}