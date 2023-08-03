using MediatR;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Events
{
    public class RegisteredNewCategoryEvent : INotification
    {
        public Guid Id { get; }
        public Category Category { get; }
        public string Detail { get; }

        public RegisteredNewCategoryEvent(Category category, string detail)
        {
            Id = Guid.NewGuid();
            Category = category;
            Detail = detail;
        }
    }
}