using FluentValidation.Results;
using MediatR;

namespace Newme.Catalog.Application.Commands
{
    public class DeactivateProductCommand : Command, IRequest<ValidationResult>
    {
        public Guid Id { get; protected set; }

        public DeactivateProductCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            if (Id == null) return false;
            return true;
        }
    }
}