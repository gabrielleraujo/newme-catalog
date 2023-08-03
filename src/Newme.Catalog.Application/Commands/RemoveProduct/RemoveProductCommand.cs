using FluentValidation.Results;
using MediatR;

namespace Newme.Catalog.Application.Commands
{
    public class RemoveProductCommand : Command, IRequest<ValidationResult>
    {
        public Guid Id { get; protected set; }

        public RemoveProductCommand(Guid id)
        {
            Id = id;
        }
    }
}