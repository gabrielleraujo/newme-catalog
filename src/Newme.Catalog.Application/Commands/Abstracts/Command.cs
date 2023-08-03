using FluentValidation.Results;

namespace Newme.Catalog.Application.Commands;

public abstract class Command
{
    protected Command()
    {
        ValidationResult = new ValidationResult();
    }

    public ValidationResult ValidationResult { get; protected set; }

    public virtual bool IsValid()
    {
        return true;
    }
}