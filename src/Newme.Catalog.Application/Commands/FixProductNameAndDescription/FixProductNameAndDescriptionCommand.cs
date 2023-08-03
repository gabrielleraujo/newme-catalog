using FluentValidation.Results;
using MediatR;

namespace Newme.Catalog.Application.Commands
{
    public class FixProductNameAndDescriptionCommand : Command, IRequest<ValidationResult>
    {
        public FixProductNameAndDescriptionCommand(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}