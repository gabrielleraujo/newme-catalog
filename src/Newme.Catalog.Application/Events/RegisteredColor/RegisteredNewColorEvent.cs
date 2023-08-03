using MediatR;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Events
{
    public class RegisteredNewColorEvent : INotification
    {
        public Guid Id { get; }
        public Color Color { get; }
        public string Detail { get; }

        public RegisteredNewColorEvent(Color color, string detail)
        {
            Id = Guid.NewGuid();
            Color = color;
            Detail = detail;
        }
    }
}