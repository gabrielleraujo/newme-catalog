using MediatR;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Events
{
    public class RegisteredNewSizeEvent : INotification
    {
        public Guid Id { get; }
        public Size Size { get; }
        public string Detail { get; }

        public RegisteredNewSizeEvent(Size size, string detail)
        {
            Id = Guid.NewGuid();
            Size = size;
            Detail = detail;
        }
    }
}