using MediatR;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Events
{
    public class ChangedProductEvent : INotification
    {
        public Guid Id { get; }
        public Product Product { get; }
        public string Detail { get; }

        public ChangedProductEvent(Product product, string detail)
        {
            Id = Guid.NewGuid();
            Product = product;
            Detail = detail;
        }
    }
}